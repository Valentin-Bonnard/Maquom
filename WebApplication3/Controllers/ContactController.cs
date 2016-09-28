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
    public class ContactController : Controller
    {
        private ICompanyRepository _companyRepository;
        private IClientRepository _clientRepository;
        private IContactRepository _contactRepository;
        private IApplicationRepository _applicationrepository;

        public ContactController(ICompanyRepository companyRepo,
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
        [HttpGet(Name = "GetContact")]
        public IActionResult Get()
        {
            var contact = _contactRepository.Getall().OrderByDescending(c => c.ID);

            if (contact != null)
            {
                return new NotFoundResult();
            }
            else
            {
               
                return Ok();
            }

        }
    }
}
