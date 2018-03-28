using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitTestingApp.Models;

namespace UnitTestingApp.Controllers
{
    public class PersonController : Controller
    {
        IPersonRepository personRepository;
        public PersonController(): this(new PersonRepository()) { }

        public PersonController(IPersonRepository personRepository)
        {
            personRepository = personRepository;
        }

        // GET: Person
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["ControllerName"] = this.ToString();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "BusinessEntityID" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var people = from s in personRepository.GetAllPersons()
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                people = people.Where(s => s.FirstName.ToUpper().Contains(searchString.ToUpper()) || s.FirstName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "First Name":
                    people = people.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    people = people.OrderBy(s => s.BusinessEntityID);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View("Index", people);
            //return View("Index",people.ToPagedList(pageNumber,pageSize));
        }

        public ViewResult Details(int id)
        {
            Person person = personRepository.GetPersonByID(id);
            return View(person);
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    personRepository.InsertPerson(person);
                    personRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Some error occured");
            }
            return View("Create", person);
        }

        public ActionResult Edit(int id)
        {
            Person person = personRepository.GetPersonByID(id);
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person person)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    personRepository.UpdatePerson(person);
                    personRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Some error occured");
            }
            return View(person);
        }

        public ActionResult Delete(bool ? saveChangesError = false,int id = 0)
        {
            if(saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Some error occured";
            }
            Person person = personRepository.GetPersonByID(id);
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Person person = personRepository.GetPersonByID(id);
                personRepository.DeletePerson(id);
                personRepository.Save();
            }
            catch(Exception ex)
            {
                return RedirectToAction("Delete", new
                {
                    id = id, saveChangesError = true
                });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            personRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}