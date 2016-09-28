using Maquom.Data.Abstract;
using Maquom.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Extension
{
    /*
     * Retrouve l'utilisateur par son nom d'utilisateur
     */
    public static class UserExtension
    { 
        public static User GetSingleByUsername(this IEntityBaseRepository<User> userRepository, string username)
        {
            return userRepository.Getall().FirstOrDefault(x => x.Username == username);
        }
    }
}
