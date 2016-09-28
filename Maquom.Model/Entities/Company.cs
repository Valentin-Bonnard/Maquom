using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maquom.Model.Entities
{
    /*
     * One-To-Many utilisant Fluent Api  
     * Entreprise contient un certain nombre d'information
     * Elle peut avois plusieurs client, application ou demande de contact
     */
    public class Company : IEntityBase
    {
        public Company()
        {
            Clients = new List<Client>();
            Applications = new List<Application>();
            Contacts = new List<Contact>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }
        public string Telephone { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int ClientID { get; set; }
        public int ApplicationID { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
