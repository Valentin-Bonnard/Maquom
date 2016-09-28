using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Abstract
{
    /*
     * Simple service de chiffrement, qui crée le "salts" et chiffre le 
     * mot de passe .
     */
    public interface IEncryptionService
    {
        string CreateSalt();
        string EncryptPassword(string password, string salt);
    }
}
