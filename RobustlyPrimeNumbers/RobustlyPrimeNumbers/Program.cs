using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobustlyPrimeNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int nr = 0;
            Console.Write("Number'th:");

            var isNumeric = Int32.TryParse(Console.ReadLine(), out nr);
            if (isNumeric)
            {
                var start = DateTime.Now;
                RPN rpn = new RPN(nr);
                Console.WriteLine("Total Seconds:" + (DateTime.Now - start).TotalSeconds);
                rpn.PrintLengthElement();
            }
        }
    }
}
