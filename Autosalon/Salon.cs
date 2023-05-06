using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Autosalon
{
    class Salon
    {
        public List<Car> Cars { get; private set; }

        public List<Customer> Customers { get; private set; }

        public Salon()
        {
            if (!File.Exists("data.json"))
            {
                File.Create("data.json").Close();
                Cars = new List<Car> { };
            }
            else
            {
                string json = File.ReadAllText("data.json");
                Cars = JsonConvert.DeserializeObject<List<Car>>(json);
                if (Cars == null)
                {
                    Cars = new List<Car> { };
                }
            }
            if (!File.Exists("customers.json"))
            {
                File.Create("customers.json").Close();
                Customers = new List<Customer> { };
            }
            else
            {
                string customerjson = File.ReadAllText("customers.json");
                Customers = JsonConvert.DeserializeObject<List<Customer>>(customerjson);
                if(Customers == null)
                {
                    Customers = new List<Customer> { };
                }
            }
        }

        private void _AddCar(int year, double price, string name, double arendPrice, double discountPrice, string mark)
        {
            if(price > 0 && arendPrice > 0)
            {
                Car newCar = new Car(ChecKValidId(), year, price, name, discountPrice, arendPrice, mark);
                Cars.Add(newCar);
                Log.Success("New car added");
            }
            else
            {
                Log.Warning("Prica can't be negative");

            }
            Console.WriteLine();
        }

        public void AddCar()
        {
            Console.Write("Please enter car creation year: ");
            int creation_year = Validator.IntValidation();
            Console.Write("Please enter car price: ");
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
                Log.Warning("Discount price can't be more that usual price");
                Console.WriteLine();
            }
            else if (arendPrice > price)
            {
                Log.Warning("Arend price can't be more that usual price");
                Console.WriteLine();
            }
            else if (creation_year < 1886)
            {
                Console.WriteLine();
                Log.Warning("The first car was created in 1886");
                Console.WriteLine();
            }
            else if (creation_year > 2023)
            {
                Console.WriteLine();
                Log.Warning("Are you from future?");
                Console.WriteLine();
            }
            else
            {
                _AddCar(creation_year, price, name, arendPrice, discountPrice, mark);
            }
        }

        public void GetAvailableCars()
        {

            if (Cars.Count == 0)
            {
                Log.Warning("There are no cars available");
            }
            else
            {
                Console.WriteLine("Available cars: ");
                int cCount = 0;
                int aCount = 0;
                for (int i = 0; i < Cars.Count; i++)
                {
                    if (!Cars[i].Arended)
                    {
                        Log.CarInfo(Cars[i]);
                        aCount++;
                    }
                    cCount++;
                }
                if(aCount == 0 && cCount != 0)
                {
                    Log.Warning("There are no cars available");
                }
            }
            Console.WriteLine();
        }

        public void GetAllCars()
        {
            if (Cars.Count == 0)
            {
                Log.Warning("There are no cars available");
            }
            else
            {
                Console.WriteLine("Available cars: ");

                for (int i = 0; i < Cars.Count; i++)
                {
                    if (Cars[i].Arended)
                    {
                        Log.NotAvailable(Cars[i]);
                    }
                    else
                    {
                        Log.CarInfo(Cars[i]);
                    }
                    
                }

            }
            Console.WriteLine();
        }

        public void SortByPrice(bool inc)
        {
            List<Car> sCars;
            if (inc)
            {
                sCars = Cars.OrderBy(x => x.Price).ToList();
            }
            else
            {
                sCars = Cars.OrderByDescending(x => x.Price).ToList();
            }

            if (sCars.Count == 0)
            {
                Log.Warning("There are no cars available");
            }
            else
            {
                Console.WriteLine("Available cars: ");

                for (int i = 0; i < sCars.Count; i++)
                {
                    if (sCars[i].Arended)
                    {
                        Log.NotAvailable(sCars[i]);
                    }
                    else
                    {
                        Log.CarInfo(sCars[i]);
                    }
                }

            }
            Console.WriteLine();
        }

        public void SortByYear(bool inc)
        {
            List<Car> sCars;
            if (inc)
            {
               sCars = Cars.OrderBy(x => x.CreationYear).ToList();
            }
            else
            {
               sCars = Cars.OrderByDescending(x => x.CreationYear).ToList();
            }
            
            if (sCars.Count == 0)
            {
                Log.Warning("There are no cars available");
            }
            else
            {
                Console.WriteLine("Available cars: ");

                for (int i = 0; i < sCars.Count; i++)
                {
                    if (sCars[i].Arended)
                    {
                        Log.NotAvailable(sCars[i]);
                    }
                    else
                    {
                        Log.CarInfo(sCars[i]);
                    }
                }

            }
            Console.WriteLine();
        }

        public void ArendCar(string id, Customer customer)
        {
            bool found = false;
            for (int i = 0; i < Cars.Count; i++)
            {
                if (Cars[i].Id == id &&!Cars[i].Arended)
                {
                    if(customer.Money >= Cars[i].ArendPrice)
                    {
                        Cars[i].Arended = true;
                        customer.Money -= Cars[i].ArendPrice;
                        customer.AddArendedCar(Cars[i]);
                        found = true;
                        Log.Success("Car arended");
                        break;
                    }
                    else
                    {
                        found = true;
                        Log.Warning("Not enough money to arend a car");
                        break;
                    }
                }
            }
            if (!found)
            {
                Log.Warning("Car not found");
            }
            Console.WriteLine();
        }

        public void ReturnCar(string id, Customer customer)
        {
            bool found = false;
            for (int i = 0; i < customer.ArendedCars.Count; i++)
            {
                if (customer.ArendedCars[i].Id == id && customer.ArendedCars[i].Arended)
                {
                    Cars.Find(item => item.Id == id).Arended = false;
                    found = true;
                    customer.ArendedCars.Remove(customer.ArendedCars[i]);
                    Log.Success("Car returned");
                    break;
                }
            }
            if (!found)
            {
                Log.Warning("Car isn't found");
            }
            Console.WriteLine();
        }

        public void BuyCar(string id, Customer customer)
        {
            for (int i = 0; i < Cars.Count; i++)
            {
                if (Cars[i].Id == id && !Cars[i].Arended)
                {
                    if(Cars[i].DiscountPrice != 0)
                    {
                        if (customer.Money < Cars[i].DiscountPrice)
                        {
                            Log.Warning("Not enough money");
                            return;
                        }
                        else
                        {
                            Log.Success("Car bought");
                            customer.Money -= Cars[i].DiscountPrice;
                            Cars.Remove(Cars[i]);
                            return;
                        }
                    }
                    else
                    {
                        if (customer.Money < Cars[i].Price)
                        {
                            Log.Warning("Not enough money");
                            return;
                        }
                        else
                        {
                            Log.Success("Car bought");
                            customer.Money -= Cars[i].Price;
                            Cars.Remove(Cars[i]);
                            return;
                        }
                    }
                }
            }
            Log.Warning("Car not found");
            Console.WriteLine();
        }

        public void GetCarsByYear(int year)
        {
            List<Car> CarsWithYear = Cars.FindAll(x => x.CreationYear == year);
            if (CarsWithYear.Count == 0)
            {
                Log.Warning("There are no cars available in " + year + " year");
            }
            else
            {
                for (int i = 0; i < CarsWithYear.Count; i++)
                {
                    if (CarsWithYear[i].Arended)
                    {
                        Log.NotAvailable(CarsWithYear[i]);
                    }
                    else
                    {
                        Log.CarInfo(CarsWithYear[i]);
                    }
                }
            }
            Console.WriteLine();
        }

        public void GetCarsByYearRange(int minYear, int maxYear)
        {
            int min = minYear > maxYear ? maxYear : minYear;
            int max = minYear > maxYear ? minYear : maxYear;
            List<Car> CarsWithYear = Cars.FindAll(x => x.CreationYear <= max && x.CreationYear >= min);
            if (CarsWithYear.Count == 0)
            {
                Log.Warning("There are no cars available between " + min + " and " + max + " years");
            }
            else
            {
                Console.WriteLine("Cars between " + min + " and " + max + " years:");
                for (int i = 0; i < CarsWithYear.Count; i++)
                {
                    if (CarsWithYear[i].Arended)
                    {
                        Log.NotAvailable(CarsWithYear[i]);
                    }
                    else
                    {
                        Log.CarInfo(CarsWithYear[i]);
                    }
                }
            
            }
            Console.WriteLine();
        }

        public void GetCarsByPriceRange(double min, double max)
        {
            double minPrice = min > max ? max : min;
            double maxPrice = min > max ? min : max;
            List<Car> CarsWithPrice = Cars.FindAll(x => x.Price <= maxPrice && x.Price >= minPrice);
            Console.WriteLine("Cars with price range " + minPrice + " - " + maxPrice + ": ");
            if (CarsWithPrice.Count == 0)
            {
                Log.Warning("There are no cars available in range " + minPrice + " and " + maxPrice + " dollars");
            }
            else
            {
                for (int i = 0; i < CarsWithPrice.Count; i++)
                {
                    if (CarsWithPrice[i].Arended)
                    {
                        Log.NotAvailable(CarsWithPrice[i]);
                    }
                    else
                    {
                        Log.CarInfo(CarsWithPrice[i]);
                    }
                }
            }
            Console.WriteLine();
        }
        
        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public void GetAllCustomers()
        {
            Console.WriteLine("|---------------------------------------------");
            for (int i = 0; i < Customers.Count; i++)
            {
                Console.WriteLine("|" + Customers[i].Username);
            }
            Console.WriteLine("|---------------------------------------------");
            Console.WriteLine();
        }

        public void SaveData()
        {
            string json = JsonConvert.SerializeObject(Cars);
            File.WriteAllText("data.json", json);
            string customers = JsonConvert.SerializeObject(Customers);
            File.WriteAllText("customers.json", customers);
        }

        public void RegisterCustomer()
        {
            Console.WriteLine("Please enter your username: ");
            string username = Validator.StringValidation();
            Console.WriteLine("Please enter your password: ");
            string password = Validator.StringValidation();
            Console.WriteLine("Please repeat your password: ");
            string passwordRepeat = Validator.StringValidation();
            Console.WriteLine("Please enter your money amount (1000,2): ");
            double money = Validator.DoubleValidation();
            for (int i = 0; i < Customers.Count; i++)
            {
                if (Customers[i].Username == username)
                {
                    Log.Warning("Customer already exists");
                    return;
                }
            }
            if (password == passwordRepeat)
            {
                Log.Success("Registration successfull\nPlease Login");
                AddCustomer(new Customer(username, money, password));
            }
            else
            {
                Console.WriteLine("Passwords don't match");
            }
        }

        private string GenerateCarID()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);

            string randomCode = string.Join("", new char[] { (char)(random.Next(0, 25) + 65), (char)(random.Next(0, 25) + 65) });

            string carID = $"{randomCode}-{randomNumber}";

            return carID;
        }

        private string ChecKValidId()
        {
            while (true)
            {
                bool exist = false;
                string id = GenerateCarID();
                for (int i = 0; i < Cars.Count; i++)
                {
                    if (Cars[i].Id == id)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    return id;
                }
            }
        }       

    }
}