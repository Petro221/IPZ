using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class McDonaldsDataManager
    {
        public List<Model.MainDish> SelectMainDishes()
        {
            try
            {
                using (var context = new McDonaldsDbEntities())
                {
                    return context.MainDishes.Select(
                        d => new Model.MainDish()
                        {
                            Id = d.Id,
                            name = d.name
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Model.MainDish>();
            }
        }

        public List<Model.CategoryDish> SelectCategoryDishes(int mainDishId)
        {
            try
            {
                using (var context = new McDonaldsDbEntities())
                {
                    return context.CategoryDishes
                        .Where(cd => cd.main_dish_id == mainDishId)
                        .Select(
                        c => new Model.CategoryDish()
                        {
                            Id = c.Id,
                            name = c.name,
                            main_dish_id = c.main_dish_id,
                            number = c.number,
                            price = c.price
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Model.CategoryDish>();
            }
        }

        public Model.User SelectUserByEmailAndPassword(string email, string password)
        {
            try
            {
                using (var context = new McDonaldsDbEntities())
                {
                    return context.Users.Where(
                        u => u.email.Equals(email) && u.password.Equals(password)
                             ).Select(u => new Model.User()
                    {
                        Id = u.Id,
                        first_name = u.first_name,
                        last_name = u.last_name,
                        email = u.email,
                        password = u.password
                    }).First();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void RemoveFoodFromCategoryDishes(int categoryDishId, int number)
        {
            try
            {
                using (var context = new McDonaldsDbEntities())
                {
                    var categoryDish = context.CategoryDishes.FirstOrDefault(d => d.Id == categoryDishId);
                    if (categoryDish != null)
                    {
                        categoryDish.number -= number;
                        context.CategoryDishes.Attach(categoryDish);
                        context.Entry(categoryDish).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public Model.Order CreateOrder(decimal sumPrice, bool eatIn, string firstName, string lastName)
        {
            try
            {
                using (var context = new McDonaldsDbEntities())
                {
                    var order = new Order();
                    order.sum_price = sumPrice;
                    order.order_date = DateTime.Now;
                    order.eat_in = eatIn;
                    order.is_payed = false;
                    order.customer_first_name = firstName;
                    order.customer_last_name = lastName;

                    Order res = context.Orders.Add(order);
                    context.SaveChanges();
                    
                    return new Model.Order()
                    {
                        eat_in = res.eat_in,
                        Id = res.Id,
                        order_date = res.order_date,
                        is_payed = res.is_payed,
                        sum_price = res.sum_price
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void CreateOrderedFood(List<Model.OrderedFood> orderedFood)
        {
            try
            {
                using (var context = new McDonaldsDbEntities())
                {
                    var dbOrderedFood = new List<OrderedFood>();
                    orderedFood.ForEach(food =>
                    {
                        OrderedFood tempOrderedFood = new OrderedFood()
                        {
                            order_id = food.order_id,
                            category_id = food.category_id,
                            number = food.number
                        };
                        dbOrderedFood.Add(tempOrderedFood);
                    });

                    context.OrderedFoods.AddRange(dbOrderedFood);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CreateUser(Model.User user)
        {
            try
            {
                using (var context = new McDonaldsDbEntities())
                {
                    var newUser = new User()
                    {
                        Id = 2,
                        first_name = user.first_name,
                        last_name = user.last_name,
                        email = user.email,
                        password = user.password
                    };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public int FinishPayment(string cardNumber, string cardName, string cardDate, int orderId)
        {
            try
            {
                using (var context = new McDonaldsDbEntities())
                {
                    var res = context.BankCreditCards.First(c => c.credit_card_number.Equals(cardNumber) &&
                                                                 c.credit_card_name.Equals(cardName) &&
                                                                 c.credit_card_date.Equals(cardDate));
                    var order = context.Orders.FirstOrDefault(o => o.Id == orderId);
                    if (res != null && order != null)
                    {
                        order.is_payed = true;
                        order.ticket_number = new Random().Next(1, 999);
                        context.Orders.Attach(order);
                        context.Entry(order).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return order.ticket_number;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
       
    }
}
