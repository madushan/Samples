using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton
{
    public class Employee
    {
        private static Employee instance = null;
        private Employee() { }
        public static Employee GetInstance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Employee();
                }
                return instance;
            }
        }

        //OR
        public Employee GetEmployee()
        {
            if (instance == null)
                instance = new Employee();
            return instance;
        }
    }
}
