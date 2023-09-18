using Microsoft.AspNetCore.Mvc;
using VivesBlog.Services;
using VivesBlog.Models;

namespace VivesBlog.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleService _PeopleContext;
        public IActionResult Index()
        {
            var people = _PeopleContext.GetAll();
            return View(people);
        }

        [HttpGet("People/Create")]
        public IActionResult PeopleCreate()
        {
            return View();
        }

        [HttpPost("People/Create")]
        public IActionResult PeopleCreate(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            _PeopleContext.Create(person);

            return RedirectToAction("PeopleIndex");
        }

        [HttpGet("People/Edit/{id}")]
        public IActionResult PeopleEdit(int id)
        {
            var person = _PeopleContext.GetPerson(id);
     

            return View(person);
        }

        [HttpPost("People/Edit/{id}")]
        public IActionResult PeopleEdit(Person person,[FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            var dbPerson = _PeopleContext.Update(person,id);


            return RedirectToAction("PeopleIndex");
        }

        [HttpGet("People/Delete/{id}")]
        public IActionResult PeopleDelete(int id)
        {
            var person = _PeopleContext.GetPerson(id);

           
            return View(person);
        }

        [HttpPost("People/Delete/{id}")]
        public IActionResult PeopleDeleteConfirmed([FromRoute] int id)
        {

            _PeopleContext.Delete(id);

            return RedirectToAction("PeopleIndex");
        }
    }
}
