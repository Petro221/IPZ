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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window, OnOpenLoginViewListener
    {
        public Registration()
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel(this);
        }

        public void openLoginView()
        {
            var loginView = new LoginView();
            loginView.Show();
            this.Close();
        }
    }
}
