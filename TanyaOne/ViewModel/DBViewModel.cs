using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.UI.Popups;
using Windows.Web.Http;
using Newtonsoft.Json;
using TanyaOne.Model;

namespace TanyaOne.ViewModel
{

    class DBViewModel
    {
        private WineDataModel WineData;

        #region TokenManagement

        private const string WineToken = "WineToken";
        public void SaveTokenWithUser(string username, string token)
        {
            var vault = new PasswordVault();
            var credential = new PasswordCredential(WineToken, username, token);
            vault.Add(credential);
        }
        public void DeleteUsersToken(string username)
        {
            var vault = new PasswordVault();
            try
            {
                // Removes the credential from the password vault.
                vault.Remove(vault.Retrieve(WineToken, username));
            }
            catch (Exception)
            {
                // If no credentials have been stored with the given RESOURCE_NAME, an exception
                // is thrown.
            }
        }
        public string GetLastLoggedInUser()
        {
            string userName = null;
            var vault = new PasswordVault();
            try
            {
                var credential = vault.FindAllByResource(WineToken).FirstOrDefault();
                if (credential != null)
                {
                    // Retrieves the actual userName and password.
                    userName = credential.UserName;
                }
            }
            catch (Exception)
            {
                // If no credentials have been stored with the given RESOURCE_NAME, an exception
                // is thrown.
            }
            return userName;
        }
        public string GetUsersToken(string username)
        {
            string token = null;

            var vault = new PasswordVault();
            try
            {
                token = vault.Retrieve(WineToken, username).Password;
            }
            catch (Exception)
            {
                // If no credentials have been stored with the given RESOURCE_NAME, an exception
                // is thrown.
            }
            return token;
        }
        #endregion


        public DBViewModel()
        {
            WineData = new WineDataModel();
        }

        #region GetDataFromServer

        public async void SaveTokenFromServer(string email,string password)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri("http://winedata.mindit.hu/ws/auth");
            string httpResponseBody;

            try
            {
                string content = "{\"email\":\""+email+"\",\"password\":\""+password+"\"}";
                var httpResponse = await httpClient.PostAsync(requestUri, new HttpStringContent(content, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
            var tokenJson = JsonConvert.DeserializeObject<TokenJson>(httpResponseBody);

            SaveTokenWithUser(email,tokenJson.token);
        }

        #endregion

    }
}
