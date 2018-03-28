using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UnitTestingApp.Controllers;
using UnitTestingApp.Models;
using UnitTestingApp.Tests.Models;

namespace UnitTestingApp.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTest
    {
        [TestMethod]
        public void IndexView()
        {
            var personController = GetPersonController(new InMemoryPersonRepository());
            ViewResult result = personController.Index(null, null, null, null);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        private static PersonController GetPersonController(IPersonRepository personRepository)
        {
            PersonController personController = new PersonController(personRepository);
            personController.ControllerContext = new System.Web.Mvc.ControllerContext()
            {
                Controller = personController,
                RequestContext = new System.Web.Routing.RequestContext(new MockHttpContext(), new System.Web.Routing.RouteData())
            };
            return personController;
        }

        [TestMethod]
        public void GetAllPeopleFromRepository()
        {
            Person person1 = GetPersonName(1,"EM",false,null,"D","E","F",null,2,null,null,Guid.NewGuid(),DateTime.Now);
            Person person2 = GetPersonName(2, "EM", false, null, "Q", "W", "E", null, 2, null, null, Guid.NewGuid(), DateTime.Now);

            InMemoryPersonRepository personRepository = new InMemoryPersonRepository();
            personRepository.InsertPerson(person1);
            personRepository.InsertPerson(person2);
            var controller = GetPersonController(personRepository);
            var result = controller.Index(null, null, null, null);
            var datamodel = (IEnumerable<Person>)result.ViewData.Model;
            CollectionAssert.Contains(datamodel.ToList(), person1);
            CollectionAssert.Contains(datamodel.ToList(), person2);
         }

        Person GetPersonName(int businessEntityId,string persontype,bool nameStyle,string title,string firstName,string middleName,string lastName,string suffix,int emailPromotion,string additionalContactInfo,string demographics,System.Guid rowGuid,DateTime modifiedDate)
        {
            return new Person
            {
                BusinessEntityID = businessEntityId,
                PersonType = persontype,
                NameStyle = nameStyle,
                Title = title,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Suffix = suffix,
                EmailPromotion = emailPromotion,
                AdditionalContactInfo = additionalContactInfo,
                Demographics = demographics,
                rowguid = rowGuid,
                ModifiedDate = modifiedDate
            };
        }

        [TestMethod]
        public void CreatePostPersonInRepository()
        {
            InMemoryPersonRepository inMemoryPersonRepository = new InMemoryPersonRepository();
            PersonController personController = GetPersonController(inMemoryPersonRepository);
            Person person = GetPersonID();
            personController.Create(person);
            IEnumerable<Person> people = inMemoryPersonRepository.GetAllPersons();
            Assert.IsTrue(people.Contains(person));
        }

        [TestMethod]
        public void Create_PostRedirectOnSuccess()
        {
            PersonController controller = GetPersonController(new InMemoryPersonRepository());
            Person model = GetPersonID();
            var result = (RedirectToRouteResult)controller.Create(model);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        public void ViewIsNotValid()
        {
            PersonController personController = GetPersonController(new InMemoryPersonRepository());
            personController.ModelState.AddModelError("", "Mock error message");
            Person model = GetPersonName(1, "EM", false, null, "D", "E", "F", null, 2, null, null, Guid.NewGuid(), DateTime.Now);
            var result = (ViewResult)personController.Create(model);
            Assert.AreEqual("Create", result.ViewName);
        }

        Person GetPersonID()
        {
            return GetPersonName(1, "EM", false, null, "D", "E", "F", null, 2, null, null, Guid.NewGuid(), DateTime.Now);
        }


    }
    public class MockHttpContext : HttpContextBase
    {
        private readonly IPrincipal principal = new GenericPrincipal(new GenericIdentity("someuser"), null);
        public override IPrincipal User
        {
            get
            {
                return principal;
            }
            set
            {
                base.User = value;
            }
        }
    }
}
