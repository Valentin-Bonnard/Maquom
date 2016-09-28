using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maquom.Model.Entities
{
    public class Application : IEntityBase
    {
        /*
       * One-To-Many  utilisant Fluent Api  
       * Application contient un certain nombre d'information 
       * Une application n'a qu'un seul client et une entreprise
       */
        public Application() { }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Android { get; set; }
        public string Ios { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int CompanµId { get; set; }
        public virtual Company Company { get; set; }
    }
}
