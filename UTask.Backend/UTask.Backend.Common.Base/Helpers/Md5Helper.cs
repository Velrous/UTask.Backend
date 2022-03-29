using System.Security.Cryptography;
using System.Text;

namespace UTask.Backend.Common.Base.Helpers
{
    public static class Md5Helper
    {
        public static string CalculateMd5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashList = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            foreach (var hash in hashList)
            {
                sb.Append(hash.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
