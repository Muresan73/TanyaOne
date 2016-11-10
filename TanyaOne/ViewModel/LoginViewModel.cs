using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TanyaOne.View;

namespace TanyaOne.ViewModel
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            Username = "";
            Password = "";
        }

        public async void LoginButtonClick()
        {
            if(await App.MainDbViewModel.LoginWithCreditianals(Username, Password))
            {
                ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
            }
        }
    }
}
