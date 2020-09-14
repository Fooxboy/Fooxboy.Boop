using System;
using System.Security.Cryptography;
using System.Text;

namespace Fooxboy.Boop.Server.Helpers
{
    public static class HashHelper
    {
        public static string GetSHA256(this string str)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
                var sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        public static bool VerifySHA256(this string hash, string str)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(str, hash) == 0;
        }
    }
}