using Maquom.Data.Repository;
using Maquom.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Maquom.Data.Abstract.IRepository;

namespace Maquom.Data
{
    public class CompanyRepository: EntityBaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(MaquomContext context)
           : base(context) { }
    }
}
