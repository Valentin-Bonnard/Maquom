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
    public class ApplicationController : Controller
    {

        private ICompanyRepository _companyRepository;
        private IClientRepository _clientRepository;
        private IContactRepository _contactRepository;
        private IApplicationRepository _applicationrepository;

        public ApplicationController(ICompanyRepository companyRepo,
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
            Application _app = _applicationrepository.GetSingle(id);
            if (_app != null)
            {
                return new NotFoundResult();
            }
            else
            {
                IEnumerable<Client> _client = _clientRepository.FindBy(c => c.ApplicationID == id);
                IEnumerable<Company> _comp = _companyRepository.FindBy(a => a.ApplicationID == id);

                foreach (var c in _comp)
                {
                    _companyRepository.Delete(c);
                }

                foreach (var l in _client)
                {
                    _clientRepository.Delete(l);
                }

                _applicationrepository.Delete(_app);

                _applicationrepository.Commit();

                return new NoContentResult();
            }

        }

        [Authorize(Policy = "back-end")]
        [HttpPost]
        public IActionResult Create([FromBody]Application application)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            application = new Application
            {
                ID = application.ID,
                Name = application.Name,
                Android = application.Android,
                Description = application.Description,
                Ios = application.Ios,
                ClientId = application.ClientId,
                Image = application.Image
            };

            _applicationrepository.Add(application);
            _applicationrepository.Commit();

            /* CreatedAtRouteResult result = CreatedAtRoute("GetnewApplication", new { controller = "Application", id = application.ID }, application); */
            return Ok();
        }

        [Authorize(Policy = "back-end")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, Application app)
        {
            var appMod = _applicationrepository.FindBy(c => c.ID == id);
            if (appMod != null)
            {
                return NotFound();
            }

            app = new Application
            {
                ID = app.ID,
                Name = app.Name,
                Android = app.Android,
                Description = app.Description,
                Ios = app.Ios,
                ClientId = app.ClientId,
                Image = app.Image
            };

            _applicationrepository.Update(app);
            _applicationrepository.Commit();
            /* CreatedAtRouteResult result = CreatedAtRoute("GetModifiedApplication", new { controller = "Application", id = Application.ID }, app); */
            return Ok();
        }
    }
}


