using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitTestingApp.Models
{
    public class PersonRepository : IPersonRepository, IDisposable
    {
        AdventureWorks2012Entities context = new AdventureWorks2012Entities();

        public void DeletePerson(int id)
        {
            Person person = context.People.Find(id);
            context.People.Remove(person);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    context.Dispose();
                }
            }
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return context.People.ToList();
        }

        public Person GetPersonByID(int id)
        {
            return context.People.Find(id);
        }

        public void InsertPerson(Person person)
        {
            context.People.Add(person);
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public void UpdatePerson(Person person)
        {
            context.Entry(person).State = System.Data.Entity.EntityState.Modified;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}