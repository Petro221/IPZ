using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;

namespace McDonalds
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window, OnOpenMainView
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
        }

        public void openMainView()
        {
            var mainView = new MainView();
            mainView.Show();
            this.Close();
        }

        public void openRegistrationView()
        {
            var registrationView = new Registration();
            registrationView.Show();
            this.Close();
        }
    }
}
