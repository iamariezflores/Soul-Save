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
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using Ionic.Zip;
using System.Security;
using System.DirectoryServices;
using System.Security.AccessControl;
using System.Security.Principal;
using SoulSave;

namespace SaveSoul
{
    public partial class DropBoxMain : Form
    {
        private string appKey = ConfigurationManager.AppSettings.Get("api_key");
        private string accessToken = string.Empty;
        private string authenticationURL = string.Empty;
        private BaseDropBox baseDropBox;
        private DropboxClient _client;

        //Folders
        public static string list = ConfigurationManager.AppSettings.Get("list");
        public static string _dir = ConfigurationManager.AppSettings.Get("temp");
        public static string _download = ConfigurationManager.AppSettings.Get("download");

        private LoadingDownload loadingDownload = new LoadingDownload();
        private bool isDoneValidating = false;
        private bool bFlag = false;
        private string _restoreFile = string.Empty;
        public static string downloadedFile = string.Empty;

        private static string _prefixName = string.Empty;
        private static string _paths = string.Empty;


        public DropBoxMain() {
            InitializeComponent();
        }

        //Authenticate User
        public void Authenticate() {
            try {
                if (string.IsNullOrEmpty(appKey)) {
                    MessageBox.Show("Invalid Appkey", "Soul Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if(baseDropBox == null) {
                    baseDropBox = new BaseDropBox(appKey, "SoulSave");
                    authenticationURL = baseDropBox.AuthenticationURLGenerated();
                    accessToken = baseDropBox.GenerateAccessToken();

                    CreateDefaultFolders();
                } 
            }catch (Exception e) {
                throw e;
            }
        }

        private void DropBoxMain_Load(object sender, EventArgs e) {
            //Create List.Txt
            File.WriteToTextFile("", list);

            //ComboBox
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;

            //ListView
            listView1.Visible = true;
            listView2.Visible = false;
            listView1.Clear();
            File.ReadTextFileAndDisplayToListView(list, listView1);
            
            //Is Logged In?
            if (SoulDB.isLoggedInDataAvaiable()) {
                CreateDefaultFolders();
            } else {
                Authenticate();
            }

            this.Size = new Size(995, 650);
            this.CenterToScreen();
        }

        //Revoke Login Credentials
        private void revokeToolStripMenuItem_Click(object sender, EventArgs e) {
            //Revoke AccessToken
            if(MessageBox.Show("Revoke?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                if (File.Delete("ConfigDB.data")) {
                    MessageBox.Show("Revoke Successful", "Revoke", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Authenticate();
                    System.Threading.Thread.Sleep(1000);
                    Application.Restart();
                } else {
                    MessageBox.Show("Revoke Failed", "Revoke", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //Create Default Folders on DropBox
        public void CreateDefaultFolders() {
            //If already Logged In
            if (SoulDB.isLoggedInDataAvaiable()) {
                _client = new DropboxClient(SoulDB.GetAccessTokenFromFile());
                if (SoulDB.isFolderExist("/DarkSouls", _client) == false) {
                    SoulDB.CreateFolder("/DarkSouls", _client);
                }

                if(SoulDB.isFolderExist("/DarkSoulsII", _client) == false) {
                    SoulDB.CreateFolder("/DarkSoulsII", _client);
                }

                if (SoulDB.isFolderExist("/DarkSoulsIII", _client) == false) {
                    SoulDB.CreateFolder("/DarkSoulsIII", _client);
                }

                if (SoulDB.isFolderExist("/Sekiro", _client) == false) {
                    SoulDB.CreateFolder("/Sekiro", _client);
                }
            } else {
                //not Logged in
                if (SoulDB.isFolderExist("/Darksouls", baseDropBox._client) == false) {
                    SoulDB.CreateFolder("/Darksouls", baseDropBox._client);
                }

                if (SoulDB.isFolderExist("/DarkSoulsII", _client) == false) {
                    SoulDB.CreateFolder("/DarkSoulsII", _client);
                }

                if (SoulDB.isFolderExist("/DarkSoulsIII", _client) == false) {
                    SoulDB.CreateFolder("/DarkSoulsIII", _client);
                }

                if (SoulDB.isFolderExist("/Sekiro", _client) == false) {
                    SoulDB.CreateFolder("/Sekiro", _client);
                }
            }
        }

        //TODO: DELETE THIS
        /*public static bool UploadToDropbox(string UploadFolder, string Filename, string Source) {
            try {
                DropboxClient _c = new DropboxClient(SoulDB.GetAccessTokenFromFile());

                if (SoulDB.isLoggedInDataAvaiable()) {
                    SoulDB.UploadFile(UploadFolder, Filename, Source, _c);       
                }

                return true;

            } catch (Exception) {
                return false;
            }
        }*/

        private void DropBoxMain_Activated(object sender, EventArgs e) {
            //Set form Size
           
        }

        //Get all files from dropbox
        public void RefreshList() {
            System.IO.File.Delete(list);
            listView1.Clear();

            var task = Task.Run((Func<Task>)this.Run);
            task.Wait();

            File.ReadTextFileAndDisplayToListView(list, listView1);
         }

        //Dropbox Get All Folder/Files
        public async Task Run() {
            //Get all files and folders from dropbox
            //bool isNoFile = false;
            File.Delete(list);

            using (var _c = new DropboxClient(SoulDB.GetAccessTokenFromFile())) {
                var listOfFoldrs = await _c.Files.ListFolderAsync(string.Empty, true);

                //get the number of folders on dropbox
                int x = listOfFoldrs.Entries.Count;

                //4 is the number of folder generated by the app
                //Darksouls, DarkSoulsII, DarkSOulsIII, Sekiro
                if(x > 4) {
                    FolderMetadata FolderInfo = new FolderMetadata();
                    FileMetadata FileInfo = new FileMetadata();

                    foreach (var item in listOfFoldrs.Entries) {
                        try {
                            if (item.IsFile) {
                                FileInfo = (FileMetadata)item;

                                File.WriteToTextFile(FileInfo.PathDisplay.ToString(), list);
                            } 
                        } catch (Exception ex) {
                            throw;
                        }
                    }
                } else {
                    MessageBox.Show("There were no files", "Soul Save", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }
               
            }
        }

        //Refresh Button
        private void btnRefresh_Click(object sender, EventArgs e) {
            //Retrieve
            
            listView2.Visible = false;
            listView1.Visible = true;
            listView1.Clear();

            var task = Task.Run((Func<Task>)this.Run);
            task.Wait();
            File.ReadTextFileAndDisplayToListView(list, listView1);
        }

        //ListView1 Double Click
        private void listView1_DoubleClick(object sender, EventArgs e) {
            //List Doubleclick
            string _s = listView1.SelectedItems[0].SubItems[0].Text;
            string _complete = _s + ".zip";
            txtListItem.Text = _complete;
        }

        //On Right-MouseClick ListView1
        private void listView1_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (listView1.FocusedItem.Bounds.Contains(e.Location)) {
                    contextMenu.Show(Cursor.Position);

                    //What list item is currently on the cursor
                    string _s = listView1.SelectedItems[0].SubItems[0].Text;
                    string _complete = _s + ".zip";
                    txtListItem.Text = _complete;
                }
            } else {
                // What list item is currently on the cursor
                string _s = listView1.SelectedItems[0].SubItems[0].Text;
                string _complete = _s + ".zip";
                txtListItem.Text = _complete;
            }
        }

        //On Right-MouseClick ListView2
        private void listView2_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (listView2.FocusedItem.Bounds.Contains(e.Location)) {
                    contextMenu.Show(Cursor.Position);

                    //What list item is currently on the cursor
                    string _s = listView2.SelectedItems[0].SubItems[0].Text;
                    string _complete = _s + ".zip";
                    txtListItem.Text = _complete;
                }
            } else {
                // What list item is currently on the cursor
                string _s = listView2.SelectedItems[0].SubItems[0].Text;
                string _complete = _s + ".zip";
                txtListItem.Text = _complete;
            }
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e) {
            //List Doubleclick
            string _s = listView2.SelectedItems[0].SubItems[0].Text;
            string _complete = _s + ".zip";
            txtListItem.Text = _complete;
        }

        //Show Preferrences
        private void archieveToolStripMenuItem_Click(object sender, EventArgs e) {
            Pref pref = new Pref();
            pref.ShowDialog();
        }

        //Rest
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e) {
           
            if (MessageBox.Show("Close Soul Save?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
                MessageBox.Show("Thank you for Using Soul Save, May the Flames Guide your Way!.");
                System.Threading.Thread.Sleep(1000);
                Application.Exit();
            }
        }

        //Upload DarkSouls
        private void darkSoulsToolStripMenuItem_Click(object sender, EventArgs e) {
            txtMode.Text = "DarkSouls";

            //Loading Form
            Loading loading = new Loading();
            loading.setTextValue(txtMode.Text);
            loading.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            loading.ShowDialog();
        }

        //Upload DarkSouls II
        private void darkSoulsIIToolStripMenuItem_Click(object sender, EventArgs e) {
            //Loading Form
            txtMode.Text = "DarkSouls2";

            Loading loading = new Loading();
            loading.setTextValue(txtMode.Text);
            loading.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            loading.ShowDialog();
        }

        //Upload DarkSoulsIII
        private void darkSoulsIIIToolStripMenuItem_Click(object sender, EventArgs e) {
            //Loading Form
            txtMode.Text = "DarkSouls3";

            Loading loading = new Loading();
            //loading.Load += new EventHandler(UploadToDropBox);
            loading.setTextValue(txtMode.Text);
            loading.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            loading.ShowDialog();
        }

        //Upload Sekiro
        private void sekiroToolStripMenuItem_Click(object sender, EventArgs e) {
            //Loading Form
            txtMode.Text = "Sekiro";

            Loading loading = new Loading();
            loading.setTextValue(txtMode.Text);
            loading.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            loading.ShowDialog();
        }

        //FormClosing Event for Loading Form/ Uploading
        private void ChildFormClosing(object sender, FormClosingEventArgs e) {
            RefreshList();
        }

        //Download from DropBox
        public async Task Download(DropboxClient _client, string source_folder, string file, string destination) {
            using (var response = await _client.Files.DownloadAsync(source_folder + "/" + file)) {
                using (var fileStream = System.IO.File.Create(destination)) {
                    (await response.GetContentAsStreamAsync()).CopyTo(fileStream);
                }
            }
        }

        //Restore Context-Menu
        private void tsDownload_Click(object sender, EventArgs e) {
            //Download from DropBox
            File.Delete(_restoreFile);
            File.Delete(downloadedFile);

            isDoneValidating = false;

            if (MessageBox.Show("Restore This File?", "Soul Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                //timer             
                timer1 = new Timer();
                timer1.Tick += new EventHandler(timer1_Tick);
                timer1.Interval = 2000;
                timer1.Start();
               
                loadingDownload.Load += new EventHandler(ProgressBarLoad);
                loadingDownload.FormClosing += new FormClosingEventHandler(LoadingFormClosing);
                loadingDownload.ShowDialog();

            }
        }

        //Form LoadingDownload FormClosing 
        private void LoadingFormClosing(object sender, FormClosingEventArgs e) {
            //Reload and Reinitialize the Form in order to removed the Lock on unzipping FIles
            //TODO: IMPROVE THIS
            this.Controls.Clear();
            InitializeComponent();
            RefreshList();
        }

        //Timer
        private void timer1_Tick(object sender, EventArgs e) {
            if (isDoneValidating) {
                loadingDownload.Hide();
                timer1.Stop();
            }
        }

        //Load the LoadingDownload Form
        private void ProgressBarLoad(object sender, EventArgs e) {
            //Generate Path
            string __itemName = txtListItem.Text;
            string _folderName = __itemName.Split('_')[0];
            string _path = "/" + _folderName;
            
            //Reference to the Downloaded File
            downloadedFile = Utilities.getApplicationPath() + @"\" + "Download" + @"\" + __itemName;

            DropboxClient client = new DropboxClient(SoulDB.GetAccessTokenFromFile());

            Task task = Task.Run(() => Download(_client, _path, __itemName, downloadedFile)).ContinueWith(t =>
                     isDoneValidating = validateHash(downloadedFile, __itemName));
        }

        //Validate the Hash of a File from DropBox Download
        public bool validateHash(string _downloadedFile, string _searchText) {
            string _hashReturn = String.Empty;
            string _finalHashReturn = String.Empty;
            string __itemName = string.Empty;
            string _folderName = string.Empty;
            string _path = string.Empty;
            bool isDoneUnzip = false;

            string _underscore = _downloadedFile.Substring(_downloadedFile.LastIndexOf('_') + 1);

            string downloadedTimestamp = Path.GetFileNameWithoutExtension(_underscore);

            try {
                //Calcualte Hash from Downloaded File
                string _hashFromfile = Utilities.CalculateMD5Hash(_downloadedFile);
                string _finalHashFromFile = _hashFromfile + "#" + downloadedTimestamp;
               
                //Read Hash.Text
                List<string> md5List = new List<string>();
                md5List = File.ReadTextFile("hash.text");

                //Loop Through Hash.Text to Find the hash that correspond to the 
                //Downloaded File.
                for (int i = 0; i < md5List.Count; i++) {
                    if (md5List[i].Contains(_searchText)) {
                        string _return = md5List[i];
                        //get the string after the + sign
                        _hashReturn = _return.Substring(_return.LastIndexOf('+') + 1);
                        //string _filename = _return.Split('+')[0];
                    }
                }
               
                //Check if the Hash from Downloadfile is the Hash from the Hash.Text
                if (_finalHashFromFile.Equals(_hashReturn)) {
                    //If its the Same then the File is Valid and can proceed to 
                    //Move and Extract the File to the Destination Folder
                    __itemName = txtListItem.Text;
                    _folderName = __itemName.Split('_')[0];
                    _path = "/" + _folderName;
                    string downloadedFile = Utilities.getApplicationPath() + @"\" + "Download" + @"\" + __itemName;

                    //Find the Correct Path
                    List<string> pathList = new List<string>();
                    pathList = File.ReadTextFile("paths.ini");

                    string _destination = String.Empty;

                    //Check on what folder to move
                    if (_folderName.Equals("DarkSouls")) {
                        _destination = pathList[0];
                        bFlag = true;
                    } else if (_folderName.Equals("DarkSoulsII")) {
                        _destination = pathList[1];
                        bFlag = true;
                    } else if (_folderName.Equals("DarkSoulsIII")) {
                        _destination = pathList[2];
                        bFlag = true;
                    } else if (_folderName.Equals("Sekiro")) {
                        _destination = pathList[3];
                        bFlag = true;
                    }

                    if (bFlag) {
                        //Copy File to Destionation
                        System.IO.File.Copy(Utilities.getApplicationPath() + @"\" + "Download" + @"\" + __itemName, _destination + @"\" + __itemName);
                        //Reference to Restore File
                        _restoreFile = _destination + @"\" + __itemName;
                        
                        //Unzip the File
                        if (Utilities.Unzip(_restoreFile, _destination)) {
                            isDoneUnzip = true;
                        }

                        if (isDoneUnzip) {
                            //Delete the Downloaded File 
                            File.Delete(Utilities.getApplicationPath() + @"\" + "Download" + @"\" + __itemName);
                        }
                       
                        bFlag = false;
                        MessageBox.Show("Restore Complete", "SoulSave", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                } else {
                    MessageBox.Show("Cannot Download File, Please Download it Again", "Soul Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Delete(_downloadedFile);
                }
                isDoneValidating = true;
                return true;
            } catch (Exception e) {
                return false;
            }
           
        }

        //Delete Button
        private void tsDelete_Click(object sender, EventArgs e) {
            //Delete from DropBox
            if(MessageBox.Show("Delete this File?", "SoulSave", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                
                DropboxClient _client = new DropboxClient(SoulDB.GetAccessTokenFromFile());
                
                //Generate Path
                string __itemName = txtListItem.Text;
                string _folderName = __itemName.Split('_')[0];
                string _path = "/" + _folderName + "/" + __itemName;

                //Timestamp from file
                string _timestamp = Path.GetFileNameWithoutExtension(__itemName.Substring(__itemName.LastIndexOf('_') + 1));
             
                if (SoulDB.Delete(_path, _client)) {
                    //Delete the hash of the file
                    if(RemoveFromHashText("hash.text", _timestamp)) {
                        MessageBox.Show("Delete Successful", "Soul Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshList();
                    }                
                } else {
                    MessageBox.Show("Cannot Delete File", "Soul Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }   
            }
        }

        //Remove Specific Hash from Hash.Text
        public static bool RemoveFromHashText(string filename, string search_string) {
            try {
                string tempFile = Path.GetTempFileName();
                using (var sr = new StreamReader(filename)) {
                    using (var sw = new StreamWriter(tempFile)) {
                        while (!sr.EndOfStream) {
                            var line = sr.ReadLine();
                            if (line.EndsWith(search_string)) {

                            } else {
                                sw.WriteLine(line);
                            }
                        }
                    }
                }

                System.IO.File.Delete(filename);
                System.IO.File.Move(tempFile, filename);

                return true;
            } catch (Exception e) {
                return false;
            }
        }

        //Combo Box Filter
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e) {
            if(System.IO.File.Exists("filter.txt")){
                File.Delete("Filter.txt");
            }
            string _filterText = string.Empty;

            if(comboBox1.Text == "All") {
                listView1.Visible = true;
                listView2.Visible = false;
  
                File.FilterListView(list, "filter.txt", _filterText, listView2);
            } else if(comboBox1.SelectedIndex == 1) {
                listView1.Visible = false;
                _filterText = "DarkSouls_";
                File.FilterListView(list, "filter.txt", _filterText, listView2);
            } else if(comboBox1.SelectedIndex == 2) {
                listView1.Visible = false;
                _filterText = "DarkSoulsII_";
                File.FilterListView(list, "filter.txt", _filterText, listView2);
            } else if(comboBox1.SelectedIndex == 3) {
                listView1.Visible = false;
                _filterText = "DarkSoulsIII_";
                File.FilterListView(list, "filter.txt", _filterText, listView2);
            } else if(comboBox1.SelectedIndex == 4) {
                listView1.Visible = false;
                _filterText = "Sekiro";
                File.FilterListView(list, "filter.txt", _filterText, listView2);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        
    }
}
