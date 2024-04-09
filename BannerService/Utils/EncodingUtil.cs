using System.Security.Cryptography;
using System.Text;

namespace BannerService.Utils
{
    public class EncodingUtil
    {
        public static string Sha256(string source)
        {
            using var hasher = SHA256.Create();
            var bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            return string.Join("", bytes.Select(x => x.ToString("x2")));
        }

        public static string EncodePassword(string password)
        {
            return Sha256(password + ConfigHelper.PasswordSalt);
        }
    }
}
