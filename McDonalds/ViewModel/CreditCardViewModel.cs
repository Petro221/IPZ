using System;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class CreditCardViewModel : BaseViewModel
    {
        public CreditCardViewModel(Order order)
        {
            CardName = "Card Name";
            FinishOrderingCommand = new Command(x =>
                {
                    var number = 
                        DataManager.FinishPayment(Numbers1 + Numbers2 + Numbers3 + Numbers4,
                            CardName, Month + '/' + Year, order.Id);
                    if (number > 0)
                    {
                        TicketNumber = "Your Ticket Number is " + number;
                        IsWrongDataError = "";
                        OnPropertyChanged(nameof(TicketNumber));
                    }
                    else
                    {
                        IsWrongDataError = "Wrong Data !";
                    }
                    OnPropertyChanged(nameof(IsWrongDataError));
                });
        }

        public string Numbers1 { get; set; }
        public string Numbers2 { get; set; }
        public string Numbers3 { get; set; }
        public string Numbers4 { get; set; }

        public string Month { get; set; }
        public string Year { get; set; }

        public string CardName { get; set; }

        public string TicketNumber { get; set; }
        public string IsWrongDataError { get; set; }

        public ICommand FinishOrderingCommand { get; set; }
    }
}
