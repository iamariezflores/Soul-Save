using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;

namespace SaveSoul
{
    class Utilities {
        public static string _dir = ConfigurationManager.AppSettings.Get("temp");

        //Compress a folder to .zip
        public static bool CompressFile(string filename, string directory) {
            try {
                using (ZipFile zip = new ZipFile()) {
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                    zip.AddDirectory(directory);
                    zip.Save(filename);

                    return true;
                }
            } catch (Exception) {
                return false;
            }
        }

        //UnZip Files
        public static bool Unzip(string _filename, string _destination) {
            try {
                ZipFile zip = ZipFile.Read(_filename);

                foreach (ZipEntry ex in zip) {
                    //if(ex.FileName == "DarkSouls_1586445162.zip") {
                    ex.Extract(_destination, ExtractExistingFileAction.OverwriteSilently);
                    //}
                }
                return true;

            } catch (Exception e) {
                return false;
            }
        }

        //Get the path to application root
        public static string getApplicationPath() {
            return Path.GetDirectoryName(Application.ExecutablePath);
        }

        //Get all Zip files from a directory
        public static IEnumerable<string> getZipFileFromDirectory() {   
            return Directory.EnumerateFiles(Utilities.getApplicationPath() + @"\" + _dir).Where(s => s.EndsWith(".zip"));
        }

        //
        public static string getFileNameFromTimestampAndString(string name, long timestamp) {
            return Utilities.getApplicationPath() + @"\" + _dir + @"\" + name + @"_" + timestamp + @".zip";
        }

        //Generate a filename with timestamp for upload 
        //_prefix is the Prefix of the filename 
        public static string generateFilenameForUpload(string _prefix) {
            string underscore = string.Empty;
            string newfilename = string.Empty;
            //var filePaths = Directory.EnumerateFiles(Utilities.getApplicationPath() + @"\Dir\").Where(s => s.EndsWith(".zip"));
            var filePaths = Utilities.getZipFileFromDirectory();
            foreach (string file in filePaths)
                underscore = file.Substring(file.IndexOf('_') + 1);

            return newfilename = _prefix + underscore;
        }

        //TODO: DELETE THIS
        /*public static string getTimeStampFromFileName(string _filename) {
            string underscore = string.Empty;
            string newfilename = string.Empty;
            string _x = string.Empty;
            
            var filePaths = Directory.EnumerateFiles(Utilities.getApplicationPath() + @"\" + _dir).Where(s => s.Contains(_filename));
            foreach (string file in filePaths)
                _x = file.Substring(file.IndexOf('_') + 1);
                //remove the file extension
                underscore = Path.GetFileNameWithoutExtension(_x);
                
            return newfilename = underscore;
        }*/

        //Generate WindowsFileTime Timestamp
        public static long getTimestamp() {
            return DateTime.Now.ToFileTime();
        }

        //Convert WindowsFileTime to DateTime
        public static DateTime convertTimestampToDate(long timestamp) {
            return new DateTime(timestamp);
        }

        //Convert UTC Timestamp to DateTime
        private static DateTime convertUtcTimeToDateTime(long utc_time) {
            System.DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return dateTime = dateTime.AddSeconds(1586318834).ToLocalTime();
        }

        public static string CalculateMD5Hash(string _file) {
            using (var md5 = System.Security.Cryptography.MD5.Create()) {
                using (var stream = System.IO.File.OpenRead(_file)) {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

    }
}
