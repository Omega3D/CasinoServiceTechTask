using CasinoServices.Application.Interfaces;
using CasinoServices.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CasinoServices.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository) 
        {
            _personRepository = personRepository; 
        }

        public async Task<IActionResult> Index()
        {
            var people = await _personRepository.GetAllAsync();
            return View(people);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                await _personRepository.CreateAsync(person);
                return RedirectToAction("Index");
            }

            return View(person);
        }
    }
}
