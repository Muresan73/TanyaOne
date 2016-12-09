using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TanyaOne.Annotations;
using TanyaOne.View;

namespace TanyaOne.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Visibility LoggingIn { get; set; }

        public LoginViewModel()
        {
            Username = "";
            Password = "";
            LoggingIn = Visibility.Collapsed;
        }

        public async void LoginButtonClick()
        {
            LoggingIn = Visibility.Visible;
            OnPropertyChanged(nameof(LoggingIn));
            if (await App.MainDbViewModel.LoginWithCreditianals(Username, Password))
            {
                ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
            }
            LoggingIn = Visibility.Collapsed;
            OnPropertyChanged(nameof(LoggingIn));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
