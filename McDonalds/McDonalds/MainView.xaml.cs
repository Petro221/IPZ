using System.Windows;
using Model;
using ViewModel;

namespace McDonalds
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window, MainViewNavigationListener
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel(this);
        }

        public void OnOpenLoginViewListener()
        {
            var loginView = new LoginView();
            loginView.Show();
            Close();
        }

        public void OnOpenCreditCardViewListener(Order order)
        {
            var creditCardView = new CreditCardView(order);
            creditCardView.Show();
        }
    }
}
