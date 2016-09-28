using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maquom.Model.Entities
{
    public class Role : IEntityBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
