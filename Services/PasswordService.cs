using System.Security.Cryptography;
using System.Text;

namespace Backend.Services
{
    public class PasswordService
    {
        public string CreateSalt()
        {
            return Guid.NewGuid().ToString();
        }

        public string HashPassword(string password, string salt)
        {
            if (password == null || salt == null) return null;
            string saltAndPwd = String.Concat(password, salt);
            HashAlgorithm algorithm = new SHA256Managed();

            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPwd)).ToString();
        }

        //public bool ValidatePassword(byte[] array1, byte[] array2)
        //{
        //    return array1.Length == array2.Length && !array1.Where((t, i) => t != array2[i]).Any();
        //}
    }
}
