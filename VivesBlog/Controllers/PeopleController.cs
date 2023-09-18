using Microsoft.AspNetCore.Mvc;
using VivesBlog_Services;
using VivesBlog_Models;

namespace VivesBlog.Ui.MVC
{
    public class PeopleController : Controller
    {
        private readonly PeopleService _peopleservice;
        public IActionResult Index()
        {
            var people = _peopleservice.GetAll();
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
            _peopleservice.Create(person);

            return RedirectToAction("PeopleIndex");
        }

        [HttpGet("People/Edit/{id}")]
        public IActionResult PeopleEdit(int id)
        {
            var person = _peopleservice.GetPerson(id);
     

            return View(person);
        }

        [HttpPost("People/Edit/{id}")]
        public IActionResult PeopleEdit(Person person,[FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            var dbPerson = _peopleservice.Update(person,id);


            return RedirectToAction("PeopleIndex");
        }

        [HttpGet("People/Delete/{id}")]
        public IActionResult PeopleDelete(int id)
        {
            var person = _peopleservice.GetPerson(id);

           
            return View(person);
        }

        [HttpPost("People/Delete/{id}")]
        public IActionResult PeopleDeleteConfirmed([FromRoute] int id)
        {

            _peopleservice.Delete(id);

            return RedirectToAction("PeopleIndex");
        }
    }
}
