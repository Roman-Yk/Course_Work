﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Autosalon
{
    class Program
    {
        static void Main(string[] args)
        {
            Salon salon = new Salon();
            bool Logged = false;
            Customer ActiveCustomer = null;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ConsoleExit);
            while (true)
            {
               
                if (!Logged)
                {
                    Console.WriteLine("Please Register or login: ");
                    Console.WriteLine("1) Register");
                    Console.WriteLine("2) Login ");
                    string user_input = Console.ReadLine();
                    if (user_input == "1")
                    {
                        Console.WriteLine("Please enter your username: ");
                        string username = Validator.StringValidation();
                        Console.WriteLine("Please enter your password: ");
                        string password = Validator.StringValidation();
                        Console.WriteLine("Please repeat your password: ");
                        string passwordRepeat = Validator.StringValidation();
                        Console.WriteLine("Please enter your money amount (1000,2): ");
                        double money = Validator.DoubleValidation();
                        if (password == passwordRepeat)
                        {
                            Console.WriteLine("Registration successfull");
                            ActiveCustomer = new Customer(username, money, password);
                            salon.AddCustomer(ActiveCustomer);
                            Logged = true;
                        }
                        else
                        {
                            Console.WriteLine("Passwords don't match");
                        }
                    }
                    else if (user_input == "2")
                    {
                        Console.WriteLine("Please enter your username: ");
                        string username = Validator.StringValidation();
                        Console.WriteLine("Please enter your password: ");
                        string password = Validator.StringValidation();
                        int index = salon.Customers.FindIndex(item => item.Username == username && item.Password == password);
                        if (index >= 0)
                        {
                            ActiveCustomer = salon.Customers.Find(cust => cust.Username == username && cust.Password == password);
                            Logged = true;
                        }
                        else
                        {
                            Console.WriteLine("Customer doesn't exist");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong option");
                    }
                }
                else
                {
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1) Get list of available cars");
                    Console.WriteLine("2) Get list of all cars");
                    Console.WriteLine("3) Get list of all cars sorted by price in increasing order");
                    Console.WriteLine("4) Get list of all cars sorted by price in descending order");
                    Console.WriteLine("5) Get list of all cars sorted by creation year in increasing order");
                    Console.WriteLine("6) Get list of all cars sorted by creation year in descending order");
                    Console.WriteLine("7) Get list of cars by certain year");
                    Console.WriteLine("8) Get list of cars by year range");
                    Console.WriteLine("9) Get list of cars by price range");
                    Console.WriteLine("10) Arend car");
                    Console.WriteLine("11) Return car");
                    Console.WriteLine("12) Add car");
                    Console.WriteLine("13) Buy car");
                    Console.WriteLine("14) Get all customers");
                    Console.WriteLine("15) Get my arended cars");
                    Console.WriteLine("16) Get my balance");
                    Console.WriteLine("Write 'exit' to quit");
                    string user_input = Console.ReadLine();
                    if (user_input == "1")
                    {
                        salon.GetAvailableCars();
                    }
                    else if (user_input == "2")
                    {
                        salon.GetAllCars();
                    }
                    else if (user_input == "3")
                    {
                        salon.SortByIncreasingPrice();
                    }
                    else if (user_input == "4")
                    {
                        salon.SortByDescendingPrice();
                    }
                    else if (user_input == "5")
                    {
                        salon.SortByIncreasingYear();
                    }
                    else if (user_input == "6")
                    {
                        salon.SortByDescendingYear();
                    }
                    else if (user_input == "7")
                    {
                        Console.WriteLine("Please enter the year of creation");
                        int year = Validator.IntValidation();
                        salon.GetCarsByYear(year);
                    }
                    else if (user_input == "8")
                    {
                        Console.Write("Please enter the min year of creation: ");
                        int min_year = Validator.IntValidation();
                        Console.Write("Please enter the max year of creation: ");
                        int max_year = Validator.IntValidation();
                        salon.GetCarsByYearRange(min_year, max_year);
                    }
                    else if (user_input == "9")
                    {
                        Console.Write("Please enter the min price: ");
                        int min_price = Validator.IntValidation();
                        Console.Write("Please enter the max price (111,5): ");
                        int max_price = Validator.IntValidation();
                        salon.GetCarsByPriceRange(min_price, max_price);
                    }
                    else if (user_input == "10")
                    {
                        Console.Write("Please enter car id: ");
                        int id = Validator.IntValidation();
                        salon.ArendCar(id, ActiveCustomer);
                    }
                    else if (user_input == "11")
                    {
                        Console.Write("Please enter car id: ");
                        int id = Validator.IntValidation();
                        salon.ReturnCar(id, ActiveCustomer);
                    }
                    else if (user_input == "12")
                    {
                        Console.Write("Please enter car creation year: ");
                        int creation_year = Validator.IntValidation();
                        Console.Write("Please enter car price (111,5): ");
                        double price = Validator.DoubleValidation();
                        Console.Write("Please enter car name: ");
                        string name = Validator.StringValidation();
                        Console.Write("Please enter car mark: ");
                        string mark = Validator.StringValidation();
                        Console.Write("Please enter car arend price: ");
                        double arendPrice = Validator.DoubleValidation();
                        Console.Write("Please enter car with discount price if any discount present else write 0: ");
                        double discountPrice = Convert.ToDouble(Console.ReadLine());
                        if (discountPrice > price)
                        {
                            Console.WriteLine("Discount price can't be more that usual price");
                        }
                        else if (creation_year < 1886)
                        {
                            Console.WriteLine();
                            Console.WriteLine("|---------------------------------------------");
                            Console.WriteLine("The first car was created in 1886");
                            Console.WriteLine("|---------------------------------------------");
                            Console.WriteLine();
                        }
                        else if (creation_year > 2023)
                        {
                            Console.WriteLine();
                            Console.WriteLine("|---------------------------------------------");
                            Console.WriteLine("Are you from future?");
                            Console.WriteLine("|---------------------------------------------");
                            Console.WriteLine();
                        }
                        else
                        {
                            salon.AddCar(creation_year, price, name, arendPrice, discountPrice, mark);
                        }
                    }
                    else if (user_input == "13")
                    {
                        Console.Write("Please enter car id: ");
                        int id = Validator.IntValidation();
                        salon.BuyCar(id, ActiveCustomer);
                    }
                    else if (user_input == "14")
                    {
                        salon.GetAllCustomers();
                    }
                    else if (user_input == "15")
                    {
                        ActiveCustomer.GetMyArendedCars();
                    }
                    else if (user_input == "16")
                    {
                        ActiveCustomer.GetBalance();
                    }
                    else if (user_input == "exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("|---------------------------------------------");
                        Console.WriteLine("Please, choose a correct option");
                        Console.WriteLine("|---------------------------------------------");
                    }
                }
                                
            }

            salon.SaveData();
            void ConsoleExit(object sender, EventArgs e)
            {
                salon.SaveData();
            }
        }
    }
}
