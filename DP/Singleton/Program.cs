using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Singleton Pattern ");

            Employee employee1 = Employee.GetInstance;
            Employee employee2 = Employee.GetInstance;

            if(employee1==employee2)
                Console.WriteLine("equal");
            else
                Console.WriteLine("not equal");

            Console.ReadKey();
        }
    }
}
