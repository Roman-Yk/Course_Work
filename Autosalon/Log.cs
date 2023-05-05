using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autosalon
{
    class Log
    {
        public static void Warning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|---------------------------------------------");
            Console.WriteLine($"{text}");
            Console.WriteLine("|---------------------------------------------");
            Console.ResetColor();
        }
        public static void Success(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("|---------------------------------------------");
            Console.WriteLine($"{text}");
            Console.WriteLine("|---------------------------------------------");
            Console.ResetColor();
        }

        public static void CarInfo(Car car)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("|---------------------------------------------");
            Console.WriteLine($"{car}");
            Console.WriteLine("|---------------------------------------------");
            Console.ResetColor();
        }

        public static void NotAvailable(Car car)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("|---------------------------------------------");
            Console.WriteLine($"{car}");
            Console.WriteLine("|---------------------------------------------");
            Console.ResetColor();
        }

    }
}
