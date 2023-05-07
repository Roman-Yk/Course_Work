using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autosalon
{
    class ProgramFlow
    {
        bool Logged = false;
        Customer ActiveCustomer = null;
        bool IsRun = true;
        private Salon salon;

        public ProgramFlow(Salon salon)
        {
            this.salon = salon;
        }

        public void Run()
        {
            while (IsRun)
            {

                if (!Logged)
                {
                    Console.WriteLine("Please Register or login: ");
                    Console.WriteLine("1) Register");
                    Console.WriteLine("2) Login ");
                    string user_input = Console.ReadLine();
                    if (user_input == "1")
                    {
                        salon.RegisterCustomer();
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
                            Log.Warning("Customer doesn't exist");
                        }
                    }
                    else
                    {
                        Log.Warning("Wrong option");
                    }
                }
                else
                {
                    if (ActiveCustomer.IsAdmin)
                    {
                        AdminRun();
                    }
                    else
                    {
                        CustomerRun();
                    }
                }
            }
        }

        private void AdminRun()
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
            Console.WriteLine("10) Change car info");
            Console.WriteLine("11) Return car");
            Console.WriteLine("12) Add car");
            Console.WriteLine("13) Remove car"); 
            Console.WriteLine("14) Get all customers");
            Console.WriteLine("15) Get my arended cars"); // get all arended cars from different customers
            Console.WriteLine("16) Add money to customer");
            Console.WriteLine("17) Get all bought cars");
            Console.WriteLine("Write 'exit' to quit");

            string user_input = Console.ReadLine();
            switch (user_input)
            {
                case "1":
                    salon.GetAvailableCars();
                    break;

                case "2":
                    salon.GetAllCars();
                    break;

                case "3":
                    salon.SortByPrice(true);
                    break;

                case "4":
                    salon.SortByPrice(false);
                    break;

                case "5":
                    salon.SortByYear(true);
                    break;

                case "6":
                    salon.SortByYear(false);
                    break;

                case "7":
                    salon.GetCarsByYear();
                    break;

                case "8":
                    salon.GetCarsByYearRange();
                    break;

                case "9":
                    salon.GetCarsByPriceRange();
                    break;

                case "10":
                    salon.ChangeCarInfo();
                    break;

                case "11":
                    salon.ReturnCar(ActiveCustomer);
                    break;

                case "12":
                    salon.AddCar();
                    break;

                case "13":
                    salon.RemoveCar();
                    break;

                case "14":
                    salon.GetAllCustomers();
                    break;

                case "15":
                    ActiveCustomer.GetMyArendedCars();
                    break;

                case "16":
                    salon.AddBalanceToCustomer();
                    break;

                case "17":
                    salon.GetBoughtCars();
                    break;

                case "exit":
                    IsRun = false;
                    return;

                default:
                    Log.Warning("Please, choose a correct option");
                    break;
            }
        }

        private void CustomerRun()
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
            Console.WriteLine("12) Buy car");
            Console.WriteLine("13) Get my arended cars");
            Console.WriteLine("14) Get my balance");
            Console.WriteLine("Write 'exit' to quit");

            string user_input = Console.ReadLine();
            switch (user_input)
            {
                case "1":
                    salon.GetAvailableCars();
                    break;

                case "2":
                    salon.GetAllCars();
                    break;

                case "3":
                    salon.SortByPrice(true);
                    break;

                case "4":
                    salon.SortByPrice(false);
                    break;

                case "5":
                    salon.SortByYear(true);
                    break;

                case "6":
                    salon.SortByYear(false);
                    break;

                case "7":
                    salon.GetCarsByYear();
                    break;

                case "8":
                    salon.GetCarsByYearRange();
                    break;

                case "9":
                    salon.GetCarsByPriceRange();
                    break;

                case "10":
                    salon.ArendCar(ActiveCustomer);
                    break;

                case "11":
                    salon.ReturnCar(ActiveCustomer);
                    break;

                case "12":
                    salon.AddCar();
                    break;

                case "13":
                    ActiveCustomer.GetMyArendedCars();
                    break;

                case "14":
                    ActiveCustomer.GetBalance();
                    break;

                case "exit":
                    IsRun = false;
                    return;

                default:
                    Log.Warning("Please, choose a correct option");
                    break;
            }
        }

    }
}
