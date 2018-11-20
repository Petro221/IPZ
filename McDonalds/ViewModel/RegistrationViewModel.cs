using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string _error;
        private OnOpenLoginViewListener _listener;

        public RegistrationViewModel(OnOpenLoginViewListener listener)
        {
            this._listener = listener;

            FirstName = "First Name";
            LastName = "Last Name";
            Email = "Email";
            Password = "Password";
            RepeatPassword = "Repeat Password";
            ButtonColor = "GRAY";

            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(RepeatPassword));
            OnPropertyChanged(nameof(ButtonColor));

            RegisterCommand = new Command((x) =>
            {
                if (!Password.Equals(RepeatPassword))
                {
                    Error = "Password do not match";
                    ButtonColor = "RED";
                    OnPropertyChanged(nameof(Error));
                    OnPropertyChanged(nameof(ButtonColor));
                } else if (FirstName.Equals("") || LastName.Equals("") || Email.Equals(""))
                {
                    Error = "Insert all fields";
                    ButtonColor = "GREEN";
                    OnPropertyChanged(nameof(Error));
                }
                else
                {
                    var user = new User()
                    {
                        first_name = FirstName,
                        last_name = LastName,
                        email = Email,
                        password = Password
                    };
                    DataManager.CreateUser(user);
                    _listener.openLoginView();
                }
            });

            ReturnToLoginViewCommand = new Command((x) =>
            {
                _listener.openLoginView();
            });
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Error
        {
            get { => _error; }
            set { _error = value; OnPropertyChanged(nameof(Error)); }
        }
        public string ButtonColor { get; set; }

        public ICommand RegisterCommand { get; }
        public ICommand ReturnToLoginViewCommand { get; }
    }

    public interface OnOpenLoginViewListener
    {
        void openLoginView();
    }
}
