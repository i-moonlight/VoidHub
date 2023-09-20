using System.Security.Cryptography;
using System.Text;

namespace ForumApi.Extensions
{
    public class PasswordHelper
    {
        public static string Hash(string password)
        {
            byte[] salt = new byte[16];
            RandomNumberGenerator.Create().GetBytes(salt);

            using (var sha256 = SHA256.Create())
            {
                byte[] hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt)));
                return Convert.ToBase64String(hashedPassword) + "." + Convert.ToBase64String(salt);
            }
        }

        public static bool Verify(string password, string hashedPassword)
        {
            string[] parts = hashedPassword.Split('.');
            byte[] salt = Convert.FromBase64String(parts[1]);

            using (var sha256 = SHA256.Create())
            {
                byte[] computedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt)));
                string computedHashString = Convert.ToBase64String(computedHash);
                return computedHashString == parts[0];
            }
        }
    }
}