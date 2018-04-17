using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models;

namespace Repository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;
        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public int AddEmployee(Employee employeeEntity)
        {
            int result = -1;
            if(employeeEntity != null)
            {
                _context.employees.Add(employeeEntity);
                _context.SaveChanges();
                result = employeeEntity.EmployeeId;
            }
            return result;
        }

        public void DeleteEmployee(int employeeId)
        {
            Employee employeeEntity = _context.employees.Find(employeeId);
            _context.employees.Remove(employeeEntity);
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.employees.ToList();
        }

        public Employee GetEmployeeById(int studentId)
        {
            return _context.employees.Find(studentId);
        }

        public int UpdateEmployee(Employee employeeEntity)
        {
            int result = -1;
            if(employeeEntity != null)
            {
                _context.Entry(employeeEntity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                result = employeeEntity.EmployeeId;
            }
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}