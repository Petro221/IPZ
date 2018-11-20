using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using Model;

namespace ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _error;
        private OnOpenMainView _listener;

        public LoginViewModel(OnOpenMainView listener)
        {
            this._listener = listener;
            LoginCommand = new Command((x) =>
            {
                var user = DataManager.SelectUserByEmailAndPassword(UserName, Password);
                if (user != null && UserName.Equals(user.email) && Password.Equals(user.password))
                {
                    Error = "";
                    _listener.openMainView();
                }
                else
                {
                    Error = "Wrong email or Password";
                }
            });
            RegistrationCommand = new Command((x) =>
            {
                _listener.openRegistrationView();
            });
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Error { get { return _error; } set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
            }

        public ICommand LoginCommand { get; }
        public ICommand RegistrationCommand { get; }
    }

    public interface OnOpenMainView
    {
        void openMainView();
        void openRegistrationView();
    }
}
