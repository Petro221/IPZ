using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private MainViewNavigationListener _listener;
        private MainDishes _mainDish;
        private string _categoryDish;
        private bool _eatIn;
        private string _howMany;
        private decimal sum = 0;
        private string firstName = "First Name";
        private string lastName = "Last Name";

        private List<Model.MainDish> dishes;
        private List<CategoryDish> categoryDishes = new List<CategoryDish>();
        private List<OrderedFood> orderedFoodList = new List<OrderedFood>();

        public MainViewModel(MainViewNavigationListener listener)
        {
            _listener = listener;
            HowManyFood = "0";
            Price = "$$$ Price $$$";
            
            SelectedCategoryDish = "Category Dishes";

            dishes = DataManager.SelectMainDishes();
            dishes.ForEach(d => MainDishesObservableCollection.Add(d.name));

            LogoutCommand = new Command((o => _listener.OnOpenLoginViewListener()));
            SaveCommand = new Command(x =>
            {
                CategoryDish categoryDish = 
                    categoryDishes.FirstOrDefault(c => c.name.Equals(SelectedCategoryDish));
                int number = Convert.ToInt32(HowManyFood);
                var orderedFood = new OrderedFood()
                {
                    category_id = categoryDish.Id,
                    number = number
                };
                categoryDish.number -= number;
                sum += categoryDish.price * number;
                Price = sum.ToString();

                orderedFoodList.Add(orderedFood);
                setOrderAvailabilyty();
                OnPropertyChanged(nameof(Price));
            });
            OrderCommand = new Command(x =>
            {
                var order = DataManager.CreateOrder(sum, EatIn, FirstName, LastName);
                orderedFoodList.ForEach(o => o.order_id = order.Id);
                orderedFoodList.ForEach(o => DataManager.RemoveFoodFromCategoryDishes(o.category_id, o.number));
                DataManager.CreateOrderedFood(orderedFoodList);
                listener.OnOpenCreditCardViewListener(order);
            });
        }

        public ObservableCollection<string> MainDishesObservableCollection { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> CategoryDishesObservableCollection { get; set; } = new ObservableCollection<string>();

        public ICommand LogoutCommand { get; }
        public ICommand OrderCommand { get; }
        public ICommand SaveCommand { get; }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                setOrderAvailabilyty();
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                setOrderAvailabilyty();
            }
        }

        public string HowManyFood
        {
            get { return _howMany; }
            set
            {
                _howMany = value;
                int number;
                if (categoryDishes != null && int.TryParse(value, out number))
                {
                    try
                    {
                        if (number != 0 && number <= categoryDishes.FirstOrDefault(c => c.name.Equals(SelectedCategoryDish)).number)
                        {
                            IsSaveEnabled = true;
                        }
                        else
                        {
                            IsSaveEnabled = false;
                        }

                        }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    IsSaveEnabled = false;
                }
                OnPropertyChanged(nameof(IsSaveEnabled));
            }
        }
        public string Price { get; set; }

        public bool IsSaveEnabled { get; set; }

        public bool IsOrderEnabled { get; set; }

        public bool TakeOut
        {
            get
            {
                if (_eatIn)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            set
            {
                _eatIn = false;
                OnPropertyChanged(nameof(EatIn));
            }
        }

        public bool EatIn
        {
            get { return _eatIn; }
            set { _eatIn = value; OnPropertyChanged(nameof(TakeOut)); }
        }

        public MainDishes SelectedMainDish
        {
            get { return _mainDish;}
            set
            {
                _mainDish = value;
                SetCategoryDishes();
            } }

        public string SelectedCategoryDish { get; set; }
        
        private void SetCategoryDishes()
        {
            categoryDishes.Clear();
            CategoryDishesObservableCollection.Clear();
            switch (SelectedMainDish)
            {
                case MainDishes.DRINKS:
                {
                    categoryDishes = DataManager.SelectCategoryDishes(1);
                    categoryDishes.ForEach(cd => CategoryDishesObservableCollection.Add(cd.name));
                    break;
                }
                case MainDishes.BURGERS:
                {
                    categoryDishes = DataManager.SelectCategoryDishes(2);
                    categoryDishes.ForEach(cd => CategoryDishesObservableCollection.Add(cd.name));
                        break;
                }
                case MainDishes.MENUS:
                {
                    categoryDishes = DataManager.SelectCategoryDishes(3);
                    categoryDishes.ForEach(cd => CategoryDishesObservableCollection.Add(cd.name));
                        break;
                }
            }

            OnPropertyChanged(nameof(CategoryDishesObservableCollection));
        }

        private void setOrderAvailabilyty()
        {
            if (FirstName.Length > 0 && LastName.Length > 0 && orderedFoodList.Count > 0)
            {
                IsOrderEnabled = true;
            }
            else
            {
                IsOrderEnabled = false;
            }
            OnPropertyChanged(nameof(IsOrderEnabled));
        }
    }

    public enum MainDishes
    {
        DRINKS,
        BURGERS,
        MENUS
    }

    public interface MainViewNavigationListener
    {
        void OnOpenLoginViewListener();
        void OnOpenCreditCardViewListener(Order order);
    }
}
