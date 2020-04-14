using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using System.IO;
using Ionic.Zip;

namespace SaveSoul
{
    class SoulDB
    {
        public static string GetAccessTokenFromFile() {
            string FILE_NAME = "ConfigDB.data";
            if (System.IO.File.Exists(FILE_NAME)) {
                string _string = File.Read(FILE_NAME);
                string _decrypt = Crypto.Decrypt(_string);

                return _decrypt;
            } else {
                return null;
            }
        }

        public static string GetAuthenticationURLFromFile() {
            string FILE_NAME = "ConfigDBUrl.data";
            if (System.IO.File.Exists(FILE_NAME)) {
                string _string = File.Read(FILE_NAME);

                return _string;
            } else {
                return null;
            }
        }

        public static bool isLoggedInDataAvaiable() {
            if (System.IO.File.Exists("ConfigDB.data")) {
                return true;
            } else {
                return false;
            }
        }

        public static bool isFolderExist(string path, DropboxClient client) {
            try {
                if(GetAccessTokenFromFile() == null) {
                    throw new Exception("Access Token is Null.");
                }

                if(GetAuthenticationURLFromFile() == null) {
                    throw new Exception("AuthenticationURI is Null");
                }                

                var folders = client.Files.ListFolderAsync(path);
                var result = folders.Result;
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        public static bool CreateFolder(string path, DropboxClient client) {
            try {
                if (GetAccessTokenFromFile() == null) {
                    throw new Exception("Access Token is Null.");
                }

                if (GetAuthenticationURLFromFile() == null) {
                    throw new Exception("AuthenticationURI is Null");
                }

                var folderArg = new CreateFolderArg(path);
                var folder = client.Files.CreateFolderV2Async(folderArg);
                var result = folder.Result;
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        public static bool UploadFile(string UploadFolderPath, string UploadFileName, string SourceFilePath, DropboxClient client) {
            try {
                using (var stream = new MemoryStream(System.IO.File.ReadAllBytes(SourceFilePath))) {
                    var response = client.Files.UploadAsync(UploadFolderPath + "/" + UploadFileName, WriteMode.Overwrite.Instance, body: stream);
                    var result = response.Result;
                }

                return true;
            } catch (Exception e) {
                return false;
                //throw e.InnerException;               
            }
        }

        public static bool Delete(string path, DropboxClient _client) {
            try {
                if (GetAccessTokenFromFile() == null) {
                    throw new Exception("Access Token is Null.");
                }

                if (GetAuthenticationURLFromFile() == null) {
                    throw new Exception("AuthenticationURI is Null");
                }

                var folders = _client.Files.DeleteV2Async(path);
                var result = folders.Result;

                return true;
            }catch (Exception e) {
                return false;
            }
        }

       /* public static bool Download(string SOURCE_FILEPATH, string DOWNLOAD_FOLDER, string _downloadFileName, DropboxClient _client) {
            try {
                var reponse = _client.Files.DownloadAsync(SOURCE_FILEPATH);
                var result = reponse.Result.GetContentAsStreamAsync();

                return true;
            } catch(Exception e) {
                return false;
            }   
        }*/

        

    }
}
