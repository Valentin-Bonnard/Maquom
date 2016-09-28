using Security.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    /*
     * Chiffrement du password par le "salts" et l'algo SHA256
     * du namespace System.Security.Cryptography 
     * One peut aussi utiliser le sien mais c'est plus facile pour l'instant 
     * d'utiliser celui-ci.
     */
    public class EncryptionService : IEncryptionService
    {
        public string CreateSalt()
        {
            var data = new byte[0x10];
            /*PAs sur de ça ? rng a la place de RNGCryptoServiceProvider */
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }

        public string EncryptPassword(string password, string salt)
        {
            using(var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", salt, password);
                byte[] saltedPAsswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPAsswordAsBytes));
            }
        }
    }
}
