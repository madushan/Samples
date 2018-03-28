using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingApp.Models;

namespace UnitTestingApp.Tests.Models
{
    class InMemoryPersonRepository : IPersonRepository
    {
        private List<Person> People = new List<Person>();

        public Exception ExceptionToThrow
        {
            get;
            set;
        }

        public void DeletePerson(int id)
        {
            People.Remove(GetPersonByID(id));
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    //
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return People.ToList();
        }

        public Person GetPersonByID(int id)
        {
            return People.FirstOrDefault(d => d.BusinessEntityID == id);
        }

        public void InsertPerson(Person person)
        {
            People.Add(person);
        }

        public int Save()
        {
            return 1;
        }

        public void UpdatePerson(Person person)
        {
            foreach(Person _person in People)
            {
                if (_person.BusinessEntityID == person.BusinessEntityID)
                {
                    People.Remove(_person);
                    People.Add(person);
                    break;
                }
            }
        }
    }
}
