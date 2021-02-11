using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace NextChallenge.Helpers {
    public class Security {
        public static string MD5Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public static HttpStatusCode Permission(Guid IdUser)
        {
            if (IdUser == Guid.Empty)
                return HttpStatusCode.Forbidden;

            return HttpStatusCode.OK;
        }
    }
}