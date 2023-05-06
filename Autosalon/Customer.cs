﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autosalon
{
    class Customer
    {
        public string Username { get; set; }
        public double Money { get; set; }
        public string Password { get; set; }
        public List<Car> ArendedCars { get; set; }


        public Customer(string name, double money, string password)
        {
            Username = name;
            Money = money;
            Password = password;
            ArendedCars = new List<Car> { };
        }

        public void AddArendedCar(Car car)
        {
            ArendedCars.Add(car);
        }

        public void GetMyArendedCars()
        {
            if(ArendedCars.Count == 0)
            {
                Log.Warning("You have no arended cars");
            }
            else
            {
                for (int i = 0; i < ArendedCars.Count; i++)
                {
                    Console.WriteLine("|---------------------------------------------");
                    Console.WriteLine(ArendedCars[i]);
                    Console.WriteLine("|---------------------------------------------");
                }
            }
            
            Console.WriteLine();
        }

        public void GetBalance()
        {
            Console.WriteLine("|---------------------------------------------");
            Console.WriteLine("Balance: " + Money);
            Console.WriteLine("|---------------------------------------------");
            Console.WriteLine();
        }

    }
}
