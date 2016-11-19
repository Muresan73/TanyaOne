using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.UI.Popups;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using Newtonsoft.Json;
using TanyaOne.Model;
using TanyaOne.Services;

namespace TanyaOne.ViewModel
{
    public class DBViewModel
    {
        public bool IsLoggedIn => SecurityService.GetLastLoggedInUser() != null;
        private WineDataModel WineData;
        private readonly WineServerDataService _wineServerDataService;


        public DBViewModel()
        {
            WineData = new WineDataModel();
            _wineServerDataService = new WineServerDataService();
        }

        public async Task<FieldData[]> GetFieldListAsync()
        {
            var result = await _wineServerDataService.GetRequestAsync<FieldData[]>("http://winedata.mindit.hu/ws/field/list");
            if (result == default(FieldData[]) )
            {
                if (_wineServerDataService.LastError.Contains("401")) SecurityService.DeleteLastLoggedInUser();
                var dialog = new MessageDialog(_wineServerDataService.LastError);
                await dialog.ShowAsync();
            }
            return result;
        }

        public async Task<SummaryData> GetSummaryAsync()
        {
            var result = await _wineServerDataService.GetRequestAsync<SummaryData>("http://winedata.mindit.hu/ws/tileData/summary");
            if (result == default(SummaryData))
            {
                if (_wineServerDataService.LastError.Contains("401")) SecurityService.DeleteLastLoggedInUser();
                var dialog = new MessageDialog(_wineServerDataService.LastError);
                await dialog.ShowAsync();
            }
            return result;
        }

        public async Task<Sensor[]> GetTileDataAsync(int sonsorId)
        {
            var result = await _wineServerDataService.GetRequestAsync<Sensor[]>("http://winedata.mindit.hu/ws/tileData/"+sonsorId);
            if (result == default(Sensor[]))
            {
                if (_wineServerDataService.LastError.Contains("401")) SecurityService.DeleteLastLoggedInUser();
                var dialog = new MessageDialog(_wineServerDataService.LastError);
                await dialog.ShowAsync();
            }
            return result;
        }

        public async Task<bool> LoginWithCreditianals(string username, string password)
        {
            //var status = await wineServerDataService.SaveTokenFromServer(username, password);

                var result = await _wineServerDataService.PostRequestAsync<TokenJson>("http://winedata.mindit.hu/ws/auth",
                    new Dictionary<string, string>{ { "email", username }, { "password", password } }, false);

            if (result == default (TokenJson))
            {
                var dialog = new MessageDialog(_wineServerDataService.LastError);
                await dialog.ShowAsync();
                
                return false;
            }else
            SecurityService.SaveTokenWithUser(username, result.token);
            return true;
        }

        public async Task LogOut()
        {
            await _wineServerDataService.PostRequestAsync<TokenJson>("http://winedata.mindit.hu/ws/logout",
                    new Dictionary<string, string>(), true);
            SecurityService.DeleteLastLoggedInUser();
        }
    }
}
