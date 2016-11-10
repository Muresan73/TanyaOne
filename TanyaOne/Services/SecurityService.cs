using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace TanyaOne.Services
{
    internal static class  SecurityService
    {


        private const string WineToken = "WineToken";

        public static void SaveTokenWithUser(string username, string token)
        {
            var vault = new PasswordVault();
            var credential = new PasswordCredential(WineToken, username, token);
            vault.Add(credential);
        }

        public static void DeleteUsersToken(string username)
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

        public static string GetCurrentToken()
        {
            string token = null;
            var vault = new PasswordVault();
            try
            {
                var credential = vault.FindAllByResource(WineToken).FirstOrDefault();
                if (credential != null)
                {
                    // Retrieves the actual userName and password.
                    var userName = credential.UserName;
                    token = vault.Retrieve(WineToken, userName).Password;
                }
            }
            catch (Exception)
            {
            }
            return token;
        }

        public static string GetLastLoggedInUser()
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

    }
}
