﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autosalon
{
    class Validator
    {
        public static string StringValidation()
        {
            string? str = Console.ReadLine();
            while (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                Console.WriteLine("The field can't be empty: ");
                str = Console.ReadLine();
            }
            return str;
        }

        public static double DoubleValidation()
        {
            double num;
            while (!double.TryParse(Console.ReadLine(), out num) || num < 0)
            {
                Console.Write("Write a positive number: ");
            }
            return num;
        }

        public static int IntValidation()
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num) || num < 0)
            {
                Console.Write("Write a positive number: ");
            }
            return num;
        }
    }
}
