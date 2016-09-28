using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Principal;
using Maquom.Model.Entities;

namespace Security
{
    public class MembershipContext
    {
        /*
         * L'object "principale" encapscule l'object "identity" et les roles.
         * En gros gérer les les roles et les authentification
         */
        public IPrincipal Principal { get; set; }
        public User User { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }


    }
}
