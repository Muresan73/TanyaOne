using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using Newtonsoft.Json;
using TanyaOne.ViewModel;

namespace TanyaOne.Services
{
    class WineServerDataService
    {
        public string LastError { get; private set; } = "";


        public async Task<T> GetRequestAsync<T>(string url)
        {
            var client = new HttpClient();
            T result = default(T);
            try
            {
                client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer",
                    SecurityService.GetCurrentToken());
                var httpResponse = await client.GetAsync(new Uri(url));
                httpResponse.EnsureSuccessStatusCode();
                var json = await httpResponse.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                LastError = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                return default(T);
            }
            return result;
        }

        public async Task<T> PostRequestAsync<T>(string url, Dictionary<string, string> data, bool withAuthorization)
        {
            string content = data.Aggregate("{", (current, element) => current + $"\"{element.Key}\":\"{element.Value}\",");
            content = content.Remove(content.Length - 1);
            content += "}";


            var client = new HttpClient();
            try
            {
                if (withAuthorization)
                {
                    client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer",
                        SecurityService.GetCurrentToken());
                }

                T result = default(T);
                string json = "";

                var httpResponse = await client.PostAsync(new Uri(url),
                    new HttpStringContent(content, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
                httpResponse.EnsureSuccessStatusCode();
                json = await httpResponse.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(json);

                return result;
            }
            catch (Exception ex)
            {
                LastError = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                return default(T);
            }

        }

        public async Task<string> SaveTokenFromServer(string email, string password)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri("http://winedata.mindit.hu/ws/auth");
            string httpResponseBody;

            try
            {
                string content = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\"}";
                var httpResponse = await httpClient.PostAsync(requestUri,
                            new HttpStringContent(content, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                LastError = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                return LastError; //TODO Update
                //return false;
            }
            var tokenJson = JsonConvert.DeserializeObject<TokenJson>(httpResponseBody);

            SecurityService.SaveTokenWithUser(email, tokenJson.token);
            return "OK";
        }

        public async void DownloadFieldList()
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri("http://winedata.mindit.hu/ws/field/list");
            string httpResponseBody = "";

            httpClient.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", SecurityService.GetCurrentToken());
            try
            {
                var httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {

            }
            var dialog = new MessageDialog(httpResponseBody);
            await dialog.ShowAsync();
        }
    }
}
