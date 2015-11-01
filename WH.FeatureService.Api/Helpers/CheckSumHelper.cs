using System;
using System.Text;

namespace WH.FeatureService.Api.Helpers
{
    public class CheckSumHelper
    {
        public static string GenerateCheckSum(string originalString)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(originalString))).Replace("-", String.Empty);
            }
        }
    }
}

