using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KaeSoft.Core.Classes
{
    public static class Hash
    {
        public enum HashType
        {
            // ReSharper disable InconsistentNaming
            MD5,
            SHA1,
            SHA256,
            SHA512
            // ReSharper restore InconsistentNaming
        }

        public static string GetHash(string text, HashType hashType)
        {
            string hashString;
            switch (hashType)
            {
                case HashType.MD5:
                    hashString = GetMD5(text);
                    break;
                case HashType.SHA1:
                    hashString = GetSHA1(text);
                    break;
                case HashType.SHA256:
                    hashString = GetSHA256(text);
                    break;
                case HashType.SHA512:
                    hashString = GetSHA512(text);
                    break;
                default:
                    throw new ArgumentException(string.Format("Invalid hashType {0}", hashType), "hashType");
            }
            return hashString;
        }

        public static bool CheckHash(string original, string hashString, HashType hashType)
        {
            string originalHash = GetHash(original, hashType);
            return (originalHash == hashString);
        }

        // ReSharper disable once InconsistentNaming
        public static string GetMD5(string text)
        {
            UnicodeEncoding ue = new UnicodeEncoding();
            byte[] message = ue.GetBytes(text);

            MD5 hashString = new MD5CryptoServiceProvider();

            var hashValue = hashString.ComputeHash(message);
            return hashValue.Aggregate("", (current, x) => current + String.Format("{0:x2}", x));
        }

        // ReSharper disable once InconsistentNaming
        public static string GetSHA1(string text)
        {
            var ue = new UnicodeEncoding();
            byte[] message = ue.GetBytes(text);

            var hashString = new SHA1Managed();

            var hashValue = hashString.ComputeHash(message);
            return hashValue.Aggregate("", (current, x) => current + string.Format("{0:x2}", x));
        }

        // ReSharper disable once InconsistentNaming
        public static string GetSHA256(string text)
        {
            var ue = new UnicodeEncoding();
            var message = ue.GetBytes(text);

            var hashString = new SHA256Managed();

            var hashValue = hashString.ComputeHash(message);
            return hashValue.Aggregate("", (current, x) => current + string.Format("{0:x2}", x));
        }

        // ReSharper disable once InconsistentNaming
        public static string GetSHA512(string text)
        {
            var ue = new UnicodeEncoding();
            var message = ue.GetBytes(text);

            var hashString = new SHA512Managed();

            var hashValue = hashString.ComputeHash(message);
            return hashValue.Aggregate("", (current, x) => current + string.Format("{0:x2}", x));
        }
    }
}
