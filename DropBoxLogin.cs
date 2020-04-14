using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dropbox.Api;
using Dropbox.Api.Files;
using System.Windows.Threading;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;

namespace SaveSoul
{
    public partial class DropBoxLogin : Form
    {
        private string redirectURI = ConfigurationManager.AppSettings.Get("redirect_url");
        private string DropBoxAppKey = string.Empty;
        private string DropBoxAuthenticationURL = string.Empty;
        private string DropBoxOAuth2State = string.Empty;
        private DropboxClient dropboxClient;

        private bool appClosing = false;

        public string AccessToken { get; private set; }
        public string UserID { get; private set; }
        public bool Result { get; private set; }
        public string Uid { get; private set; }

        public DropBoxLogin(string AppKey, string AuthenticationURL, string OAuth2State) {
            InitializeComponent();
            DropBoxAppKey = AppKey;
            DropBoxAuthenticationURL = AuthenticationURL;
            DropBoxOAuth2State = OAuth2State;
        }

        public void Navigate() {
            try {
                if (!string.IsNullOrEmpty(DropBoxAppKey)) {
                    Uri authorizeURI = new Uri(DropBoxAuthenticationURL);
                    webBrowser.Navigate(authorizeURI);
                }
            }catch (Exception) {
                throw;
            }
        }

        private void DropBoxLogin_Load(object sender, EventArgs e) {
            //Dispatcher.CurrentDispatcher.BeginInvoke(new Action(Navigate));
            //File.Delete("status.txt");
            if (!System.IO.File.Exists("ConfigDB.data")) {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(Navigate));
            } else {  
                this.Close();
            }
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
          
        }

        private void menuBack_Click(object sender, EventArgs e) {
            if (webBrowser.CanGoBack) {
                webBrowser.GoBack();
            }
        }

        private void DropBoxLogin_Activated(object sender, EventArgs e) {
            //Set the Form Size and Startup Position
            this.Size = new Size(1000, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e) {
            if (!e.Url.AbsoluteUri.ToString().StartsWith(redirectURI.ToString(), StringComparison.OrdinalIgnoreCase)) {
                return;
            }

            try {
                OAuth2Response response = DropboxOAuth2Helper.ParseTokenFragment(e.Url);

                if (response.State != DropBoxOAuth2State) {
                    return;
                }

                this.AccessToken = response.AccessToken;
                this.Uid = response.Uid;
                this.Result = true;

                appClosing = true;
            } catch (ArgumentException ae) {

            } finally {
                //e.Cancel = true;
                this.Close();
            }
        }

        private void DropBoxLogin_FormClosing(object sender, FormClosingEventArgs e) {
            switch (e.CloseReason) {
                case CloseReason.UserClosing:
                    if (appClosing) {
                        //Open DropBoxMain
                    } else {
                        if (MessageBox.Show("Close Application?", "SoulSave", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK) {
                            Application.Exit();
                        } else {
                            e.Cancel = true;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        
    }
}
