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

        [HttpGet]
        public async Task<IActionResult> Update(string personId)
        {
            var person = await _personRepository.GetByIdAsync(personId);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            var editPerson = await _personRepository.GetByIdAsync(person.Id);

            if (editPerson == null)
            {
                return NotFound();
            }

            editPerson.Name = person.Name;
            editPerson.Email = person.Email;
            editPerson.Phone = person.Phone;
            editPerson.Address = person.Address;

            await _personRepository.UpdateAsync(editPerson);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            await _personRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}