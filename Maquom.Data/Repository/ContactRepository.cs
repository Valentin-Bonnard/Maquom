using Maquom.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Maquom.Data.Abstract.IRepository;

namespace Maquom.Data.Repository
{
    public class ContactRepository : EntityBaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(MaquomContext context)
           : base(context) { }
    }
}
