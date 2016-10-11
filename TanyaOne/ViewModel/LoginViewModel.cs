using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;

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
            var dialog = new MessageDialog(Username+" : "+Password);
            await dialog.ShowAsync();
        }
    }
}
