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

namespace SaveSoul
{
    public partial class Loading : Form
    {
        public string mode;
        public static bool isDone = false;
        public static bool bStatus = false;
        public static string _dir = ConfigurationManager.AppSettings.Get("temp");

        private static string _prefixName = string.Empty;
        private static string _paths = string.Empty;

        public Loading() {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e) {
            //Timer
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 2000;
            timer1.Start();

            //ProgressBar
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 50;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;

            for (int i = 0; i < progressBar1.Maximum; i++) {
                progressBar1.Value = i;
            }

            //Run the Uploading Task
            Task task = Task.Run(() => BeginUpload(mode));
        }

        //Get Instruction from DropBoxMain for What to Download.
        public void setTextValue(string value) {
            mode = value;
            label1.Text = "Uploading " + mode + " Please wait...";
        }

        //Upload to dropBox
        public static bool UploadToDropbox(string UploadFolder, string Filename, string Source) {
            try {
                DropboxClient _c = new DropboxClient(SoulDB.GetAccessTokenFromFile());

                if (SoulDB.isLoggedInDataAvaiable()) {
                    if (SoulDB.UploadFile(UploadFolder, Filename, Source, _c)) {
                        bStatus = true;
                    } else {
                        //Delete All files on Temp Folder 
                        DirectoryInfo directoryInfo = new DirectoryInfo(Utilities.getApplicationPath() + @"\" + _dir);
                        foreach(FileInfo file in directoryInfo.EnumerateFiles()) {
                            file.Delete();
                        }

                        bStatus = false;
                    }
                }

            if (bStatus) {
                return true;
            } else {
                return false;                 
            }

            } catch (Exception) {
                return false;
            }
        }

        //Initiate the Upload
        private static void BeginUpload(string _mode) {
            //Read the paths from paths.ini
            List<string> pathList = new List<string>();
            pathList = File.ReadTextFile("paths.ini");

            if (_mode == "DarkSouls") {
                _prefixName = "DarkSouls";
                _paths = pathList[0].ToString();
            } else if(_mode == "DarkSouls2") {
                _prefixName = "DarkSoulsII";
                _paths = pathList[1].ToString();
            } else if(_mode == "DarkSouls3") {
                _prefixName = "DarkSoulsIII";
                _paths = pathList[2].ToString();
            } else if(_mode == "Sekiro") {
                _prefixName = "Sekiro";
                _paths = pathList[3].ToString();
            }

            //Delete Zip File from Directory
            //Remnants from the Restore Procedure
            string x = String.Empty;
            var filePaths = getZipFileFromDirectory(_paths);
            foreach (string file in filePaths)
                x = file.ToString();
            File.Delete(x);

            //Filename and timestamp for the Compressed File
            var timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            string filename = Utilities.getFileNameFromTimestampAndString(_prefixName, timeStamp);

            //Compress the File
            if (Utilities.CompressFile(filename, _paths)) {
                //success 
                //System.Threading.Thread.Sleep(2000);

                //filename for upload 
                string newfilename = Utilities.generateFilenameForUpload(_prefixName + "_");

                if (UploadToDropbox("/" + _prefixName, newfilename, Utilities.getApplicationPath() + @"\" + _dir + @"\" + newfilename)) {
                    //if (MessageBox.Show("Upload Complete.!", "SoulSave", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) {

                        //Write the hash of the file to Hash.Text for verification later    
                        string _md5Hash = Utilities.CalculateMD5Hash(Utilities.getApplicationPath() + @"\" + _dir + @"\" + newfilename);
                        File.WriteToTextFile(newfilename + "+" + _md5Hash + "#" + timeStamp, "hash.text");

                        //Give it a second before deleting the File.
                        System.Threading.Thread.Sleep(1000);

                        File.Delete(Utilities.getApplicationPath() + @"\" + _dir + @"\" + newfilename);

                        //notify the timer
                        isDone = true;
                    //}
                } else {
                    MessageBox.Show("Failed to Upload Files, Please check your Internet Connection.", "Soul Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isDone = true;
                }
            } else {
                //failed
               MessageBox.Show("An Error Occurred While Preparing Files for Upload.", "Soul Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
               isDone = true;
            }
        }

        //Get Zip File from Directory
        public static IEnumerable<string> getZipFileFromDirectory(string path) {
            return Directory.EnumerateFiles(path).Where(s => s.EndsWith(".zip"));
        }

        //Timer to update UI
        private void timer1_Tick(object sender, EventArgs e) {
            if (isDone) {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                if (bStatus) {
                    label1.Text = "Upload Complete";
                }else {
                    label1.Text = "Failed to Upload";
                }               
                btnClose.Visible = true;
                isDone = false;
                timer1.Stop();
            }
        }

        //Close Button
        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        //The X Button
        private void btnExit_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
