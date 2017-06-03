using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryHub
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public class Hasher
    {
        //public static string Encrypt(string text)
        //{
        //    ArtisanCode.SimpleAesEncryption.RijndaelMessageEncryptor rji = new ArtisanCode.SimpleAesEncryption.RijndaelMessageEncryptor();
        //    rji.Configuration = new ArtisanCode.SimpleAesEncryption.SimpleAesEncryptionConfiguration();
        //    rji.Configuration.EncryptionKey = new ArtisanCode.SimpleAesEncryption.EncryptionKeyConfigurationElement(256, "3q2+796tvu/erb7v3q2+796tvu/erb7v3q2+796tvu8=");
        //    return rji.Encrypt(text);
        //}



        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }




    }
}