using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Newtonsoft.Json;
using TanyaOne.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TanyaOne.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView : Page
    {
        public LoginViewModel LoginViewModelInstance => (LoginViewModel) this.DataContext;


        public LoginView()
        {
            this.InitializeComponent();
        }

        private async void amper(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri("http://winedata.mindit.hu/ws/auth");
            string httpResponseBody;

            try
            {
                //Send the GET request
                var httpResponse = await httpClient.PostAsync(requestUri,new HttpStringContent("{\"email\":\"test@mindit.hu\",\"password\":\"Qwert1986\"}",Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
            var dialog = new MessageDialog(httpResponseBody);
            await dialog.ShowAsync();
            var tokenJson = JsonConvert.DeserializeObject<ViewModel.TokenJson>(httpResponseBody);
            dialog = new MessageDialog(tokenJson.token);
            await dialog.ShowAsync();
        }

    }
    public class TokenJson { public string token { get; set; } }
}
