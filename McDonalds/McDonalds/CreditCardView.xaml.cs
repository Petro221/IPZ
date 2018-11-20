using System.Windows;
using Model;
using ViewModel;

namespace McDonalds
{
    /// <summary>
    /// Interaction logic for CreditCardView.xaml
    /// </summary>
    public partial class CreditCardView : Window
    {
        public CreditCardView(Order order)
        {
            InitializeComponent();
            DataContext = new CreditCardViewModel(order);
        }
    }
}
