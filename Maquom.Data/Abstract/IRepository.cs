using Maquom.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maquom.Data.Abstract
{
    public class IRepository
    {
        public interface ICompanyRepository : IEntityBaseRepository<Company> { }
        public interface IClientRepository : IEntityBaseRepository<Client> { }
        public interface IApplicationRepository : IEntityBaseRepository<Application> { }
        public interface IContactRepository : IEntityBaseRepository<Contact> { }
        public interface IUserRepository : IEntityBaseRepository<User> { }
        public interface IUserRoleRepository : IEntityBaseRepository<UserRole> { }
        public interface IRoleRepository : IEntityBaseRepository<Role> { }

    }
}
