using System;

namespace MultiFrameworkConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {

#if NET40
            Console.WriteLine("Target framework: .net 4.0");
#elif NET45
            Console.WriteLine("Target framework: .net 4.5");
#else
            Console.WriteLine("Target framework: .net core");
#endif

            Console.ReadKey();
        }
    }
}
