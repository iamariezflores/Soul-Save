using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace SaveSoul
{
    class File
    {
        //Save/Write to .data
        public static void Save(string input, string FILE_NAME) {
            if (System.IO.File.Exists(FILE_NAME)) {
                return;
            }

            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Create)) {
                using (BinaryWriter w = new BinaryWriter(fs)) {
                    w.Write(input);
                    //for (int i = 0; i < fs.Length; i++) {
                    //w.Write(input);
                    //}
                }
            }
        }

        //Write to Text file with Append enable
        public static void WriteToTextFile (string input, string FILE_NAME) {
            using (var writer = new StreamWriter(FILE_NAME, true)) {
                writer.Write(input);
                writer.WriteLine();
            }
        }

        //Write to text file no Append enable
        public static void WriteToTextFileNoAppend(string input, string FILE_NAME) {
            using (var writer = new StreamWriter(FILE_NAME, false)) {
                writer.Write(input);
                //writer.WriteLine();
            }
        }

        //Read from a TextFile
        public static List<String> ReadTextFile(string FILE_NAME) {
            var _string = new List<string>();

            using (var reader = new StreamReader(FILE_NAME)) {
                string result;
                while ((result = reader.ReadLine()) != null) {
                    _string.Add(result);
                }

                return _string;
            }
        }

        //TODO: DELETE THIS SOON
        /*public static string CheckStatusTxt(string FILE_NAME) {
            string _string = string.Empty;
            try {
                using(var reader = new StreamReader(FILE_NAME)) {
                    string result;
                    while((result = reader.ReadLine()) !=null) {
                        _string = result;
                    }
                }

                return _string;
            }catch(Exception e) {
                return null;
            }
        }*/

        //Read from a Textfile and Display to ListView
        public static bool ReadTextFileAndDisplayToListView(string FILE_NAME, ListView listView) {
            //Set ListView Properties
            listView.View = View.Details;
            listView.GridLines = true;
            listView.FullRowSelect = true;

            listView.Columns.Add("Files", 500) ;
            listView.Columns.Add("Details", 450);
            //->

            try {
                using (var reader = new StreamReader(FILE_NAME)) {
                    string result;
                    while ((result = reader.ReadLine()) != null) {

                        listView.Items.Add(Path.GetFileNameWithoutExtension(result));
                    }
                }

                List<string> FileList = new List<string>();
                FileList = File.ReadTextFile("list.txt");
                //MessageBox.Show(FileList.Count.ToString());

                for (int i = 0; i < FileList.Count; i++) {
                    string _s = FileList[i].ToString();
                    

                    //get the string after the last "/" 
                    int _a = _s.LastIndexOf("/") + 1;
                    string _b = _s.Substring(_a, _s.Length - _a);

                    //remove the file extension
                    string _withoutExt = Path.GetFileNameWithoutExtension(_b);

                    //get the timestamp part
                    string _x  = _withoutExt.Substring(_withoutExt.IndexOf('_') + 1);
                    string strTimestamp = _x;

                    //convert the string timestamp to dateTime
                    //conver to long
                    long lTimestamp = long.Parse(strTimestamp);

                    //convert unix time to dateTime
                    System.DateTime Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                    Timestamp = Timestamp.AddSeconds(lTimestamp).ToLocalTime();

                    //add to listview
                    listView.Items[i].SubItems.Add(Timestamp.ToString());
                }
                return true;
            } catch(Exception e) {
                return false;
            }

           
        }

        //Filter the ListView
        public static void FilterListView(string IN_FILE_NAME, string OUT_FILE_NAME, string _filterText, ListView listview) {
            listview.Visible = true;
            listview.Clear();

            List<string> FileList = new List<string>();
            FileList = File.ReadTextFile(IN_FILE_NAME);

            /*if(FileList.Count == 0) {
                MessageBox.Show("No File to Filter.", "SoulSave", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }*/


            for (int i = 0; i < FileList.Count; i++) {
                string _s = FileList[i].ToString();
                if (_s.Contains(_filterText)) {
                    File.WriteToTextFile(FileList[i].ToString(), OUT_FILE_NAME);
                }
            }

            File.ReadTextFileAndDisplayToListView(OUT_FILE_NAME, listview);
        }

        //Read from .data 
        public static string Read(string FILE_NAME) {
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read)) {
                 using (BinaryReader r = new BinaryReader(fs)) {
                    return r.ReadString();
                }
            }
        }

        //Delete a file
        public static bool Delete(string FILE_NAME) {
            try {
                if (System.IO.File.Exists(FILE_NAME)) {
                    System.IO.File.Delete(FILE_NAME);
                } 
                return true;
            } catch (Exception e) {
                return false;
            }
            
        }

        public static bool DeleteAllFileWithExtension(string path, string ext) {
            bool bStatus = false;
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] fileInfos = directoryInfo.GetFiles("*" + ext).Where(
                                    p => p.Extension == "*" + ext).ToArray();

            foreach(FileInfo file in fileInfos) {
                try {
                    file.Attributes = FileAttributes.Normal;
                    
                    File.Delete(file.FullName);
                    bStatus = true;
                }catch {
                    bStatus = false;
                }
            }

            if (bStatus) {
                return true;
            } else {
                return false;
            }
        }
    }
}
