using System.ComponentModel;
using System.Runtime.CompilerServices;
using ServerConnection;
using ViewModel.Annotations;

namespace ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected ClientBinder DataManager;

        public BaseViewModel()
        {
            DataManager = ClientBinder.GetInstance();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
