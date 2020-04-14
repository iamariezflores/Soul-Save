using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using System.Windows.Threading;
using System.Net.Http;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;

namespace SaveSoul
{
    class BaseDropBox
    {
        public DropboxClient _client;
        private string OAuth2State;
        private string redirectURI = ConfigurationManager.AppSettings.Get("redirect_url");
        private const string _AppName = "SoulSave";

        public string AppKey { get; private set; }
        public string AppSecret { get; private set; }
        public string AppName { get; private set; }
        public string AuthenticationURL { get; private set; }
        public string AccessToken { get; private set; }
        public string UID { get; private set; }

        public BaseDropBox(string ApiKey, string ApiSecret, string ApplicationName = _AppName) {
            try {
                AppKey = ApiKey;
                AppSecret = ApiSecret;
                AppName = ApplicationName; 
            }catch(Exception) {
                throw;
            }
        }

        public string AuthenticationURLGenerated() {
            try {
                this.OAuth2State = Guid.NewGuid().ToString("N"); //converts a number to a string of the form "-d,ddd,ddd.ddd…"
                Uri authorizeduri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, AppKey, redirectURI, state: OAuth2State);
                AuthenticationURL = authorizeduri.AbsoluteUri.ToString();

                //string _encrypt = Crypto.Encrypt(AuthenticationURL);
                File.Save(AuthenticationURL, "ConfigDBUrl.data");

                return authorizeduri.AbsoluteUri.ToString();
            } catch (Exception) {
                throw;
            }
        }

        public bool CanAuthenticate() {
            try {
                if(AppKey == null) {
                    throw new ArgumentNullException("App Key is Null"); 
                }

                if(AppSecret == null) {
                    throw new ArgumentNullException("App Secret is Null");
                }

                return true;
            }catch (Exception e) {
                throw e;
            }
        }

        public string GenerateAccessToken() {
            try {
                string _accessToken = string.Empty;

                if (CanAuthenticate()) {
                    if (string.IsNullOrEmpty(AuthenticationURL)) {
                        throw new Exception("Authentication URL is not generated!");
                    }

                    DropBoxLogin dropBoxLogin = new DropBoxLogin(AppKey, AuthenticationURL, this.OAuth2State);
                    dropBoxLogin.Owner = DropBoxMain.ActiveForm;
                    dropBoxLogin.ShowDialog();
                    if (dropBoxLogin.Result) {
                        _accessToken = dropBoxLogin.AccessToken;
                        AccessToken = dropBoxLogin.AccessToken;
                        UID = dropBoxLogin.Uid;
                        DropboxClientConfig _clientConfig = new DropboxClientConfig(AppKey, 1);
                        HttpClient httpClient = new HttpClient();
                        httpClient.Timeout = TimeSpan.FromMinutes(10);
                        _clientConfig.HttpClient = httpClient;
                        _client = new DropboxClient(AccessToken, _clientConfig);

                        string _crypto = Crypto.Encrypt(_accessToken);
                        File.Save(_crypto, "ConfigDB.data");

                    } else {
                        _client = null;
                        AccessToken = string.Empty;
                        UID = string.Empty;
                    }
                   
                }

                
                return _accessToken;
                
            }catch (Exception e) {
                throw e;
            }
        }
    }
}
