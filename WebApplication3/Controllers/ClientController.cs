using Maquom.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Maquom.Data.Abstract.IRepository;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {

        private ICompanyRepository _companyRepository;
        private IClientRepository _clientRepository;
        private IContactRepository _contactRepository;
        private IApplicationRepository _applicationrepository;

        public ClientController(ICompanyRepository companyRepo,
            IClientRepository clientRepo,
            IContactRepository contactRepo,
            IApplicationRepository applicationRepo)
        {
            _companyRepository = companyRepo;
            _clientRepository = clientRepo;
            _contactRepository = contactRepo;
            _applicationrepository = applicationRepo;
        }


        [Authorize(Policy = "back-end")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Client _client = _clientRepository.GetSingle(id);
            if (_client != null)
            {
                return new NotFoundResult();
            }
            else
            {
                IEnumerable<Company> _company = _companyRepository.FindBy(c => c.ClientID == id);
                IEnumerable<Application> _app = _applicationrepository.FindBy(a => a.ClientId == id);

                foreach (var e in _company)
                {
                    _companyRepository.Delete(e);
                }

                foreach (var a in _app)
                {
                    _applicationrepository.Delete(a);
                }

                _clientRepository.Delete(_client);

                _clientRepository.Commit();

                return new NoContentResult();
            }

        }

        [Authorize(Policy = "back-end")]
        [HttpPost]
        public IActionResult Create([FromBody]Client newclient)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            newclient = new Client
            {
                ID = newclient.ID,
                Name = newclient.Name,
                FirstName = newclient.FirstName,
                Address = newclient.Address,
                City = newclient.City,
                Country = newclient.Country,
                Email = newclient.Email,
                Telephone = newclient.Telephone,
                Applications = newclient.Applications

            };

            _clientRepository.Add(newclient);
            _clientRepository.Commit();

            /* CreatedAtRouteResult result = CreatedAtRoute("GetNewClient", new { controller = "Client", id = Client.ID }, client); */
            return Ok();
        }

        [Authorize(Policy = "back-end")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, Client client)
        {
            var  clientMod = _clientRepository.FindBy(c => c.ID == id);
            if(clientMod != null)
            {
                return NotFound();
            }
       
            client = new Client {
                ID = client.ID,
                Name = client.Name,
                FirstName = client.FirstName,
                Address = client.Address,
                City = client.City,
                Country = client.Country,
                Email = client.Email,
                Telephone = client.Telephone,
                Applications = client.Applications
            };

            _clientRepository.Update(client);
            _clientRepository.Commit();
            /* CreatedAtRouteResult result = CreatedAtRoute("GetModifiedClient", new { controller = "Client", id = Client.ID }, client); */
            return Ok();
        }
    }
}


