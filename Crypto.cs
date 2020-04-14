using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Configuration;
using System.Collections.Specialized;

namespace SaveSoul
{
    class Crypto
    {
        //DO NOT Change this 
        private const string _key = ">5TEHOAc+8CDG;y@";
        public static string Encrypt(string input, string key = _key) {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            tripleDESCryptoServiceProvider.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
            ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
            byte[] resultArray = cryptoTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDESCryptoServiceProvider.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string input, string key = _key) {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            tripleDESCryptoServiceProvider.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
            ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
            byte[] resultArray = cryptoTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDESCryptoServiceProvider.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
