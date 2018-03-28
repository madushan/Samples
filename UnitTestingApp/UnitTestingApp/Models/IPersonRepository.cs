using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitTestingApp.Models
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAllPersons();
        void InsertPerson(Person person);
        void DeletePerson(int id);
        void UpdatePerson(Person person);
        Person GetPersonByID(int id);
        void Dispose();
        int Save();
    }
}