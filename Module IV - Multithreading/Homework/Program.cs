using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework
{
    class Program
    {
        static void Main()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < 100; i++)
            {
                var i1 = i;
                Task.Factory.StartNew(() => { Console.WriteLine(i1); }).Wait();
            }
        }

        private static void Do()
        {
            Console.WriteLine();
        }
    }
}
