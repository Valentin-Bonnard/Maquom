using Maquom.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Abstract
{
    /*
     * Implementation d'un mechanisme servant a créer un utilisateur 
     */
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, int[] roles);
        User GetUser(int UserId);
        List<Role> GetUserRoles(string username);
    }
}
