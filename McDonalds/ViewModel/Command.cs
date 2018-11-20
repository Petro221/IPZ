using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    class Command : ICommand
    {
        private readonly Action<object> _executeDelegate;

        public Command(Action<object> executeData)
        {
            _executeDelegate = executeData;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _executeDelegate(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
