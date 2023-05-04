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

        public void AddCar(int year, double price, string name, double arendPrice, double discountPrice, string mark)
        {
            if(price > 0 && arendPrice > 0)
            {
                Console.WriteLine("|---------------------------------------------");
                Console.WriteLine("New car added");
                Console.WriteLine("|---------------------------------------------");
                Car newCar = new Car(Cars.Count+1,year, price, name, discountPrice, arendPrice, mark);
                Cars.Add(newCar);
            }
            else
            {
                Console.WriteLine("|---------------------------------------------");
                Console.WriteLine("Price can't be negative");
                Console.WriteLine("|---------------------------------------------");
                
            }
            Console.WriteLine();
        }

        public void GetAvailableCars()
        {

            if (Cars.Count == 0)
            {
                Console.WriteLine("|---------------------------------------------");
                Console.WriteLine("There are no cars available");
                Console.WriteLine("|---------------------------------------------");
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
                        Console.WriteLine("|---------------------------------------------");
                        Console.WriteLine("|Id: " + Cars[i].Id);
                        Console.WriteLine("|Name: " + Cars[i].CarName);
                        Console.WriteLine("|Mark: " + Cars[i].CarMark);
                        Console.WriteLine("|Creation Year: " + Cars[i].CreationYear);
                        Console.WriteLine("|Price: " + Cars[i].Price);
                        Console.WriteLine("|Discount: " + (Cars[i].DiscountPrice == 0 ? "No discount" : Cars[i].DiscountPrice));
                        Console.WriteLine("|Arend Price: " + Cars[i].ArendPrice);
                        Console.WriteLine("|---------------------------------------------");
                        aCount++;
                    }
                    cCount++;
                }
                if(aCount == 0 && cCount != 0)
                {
                    Console.WriteLine("|---------------------------------------------");
                    Console.WriteLine("There are no cars available");
                    Console.WriteLine("|---------------------------------------------");
                }
            }
            Console.WriteLine();
        }

        public void GetAllCars()
        {
            if (Cars.Count == 0)
            {
                Console.WriteLine("|---------------------------------------------");
                Console.WriteLine("There are no cars available");
                Console.WriteLine("|---------------------------------------------");
            }
            else
            {
                Console.WriteLine("Available cars: ");

                for (int i = 0; i < Cars.Count; i++)
                {
                    Print(Cars[i]);
                }

            }
            Console.WriteLine();
        }

        public void SortByIncreasingPrice()
        {
            List<Car> sCars = Cars.OrderBy(x => x.Price).ToList();
            if (sCars.Count == 0)
            {
                Console.WriteLine("|---------------------------------------------");
                Console.WriteLine("There are no cars available");
                Console.WriteLine("|---------------------------------------------");
            }
            else
            {
                Console.WriteLine("Available cars: ");

                for (int i = 0; i < sCars.Count; i++)
                {
                    Print(sCars[i]);
                }

            }
            Console.WriteLine();
        }

        public void SortByDescendingPrice()
        {
            List<Car> sCars = Cars.OrderByDescending(x => x.Price).ToList();
            if (sCars.Count == 0)
            {
                Console.WriteLine("|---------------------------------------------");
                Console.WriteLine("There are no cars available");
                Console.WriteLine("|---------------------------------------------");
            }
            else
            {
                Console.WriteLine("Available cars: ");

                for (int i = 0; i < sCars.Count; i++)
                {
                    Print(sCars[i]);
                }

            }
            Console.WriteLine();
        }

        public void SortByDescendingYear()
        {
            List<Car> sCars = Cars.OrderByDescending(x => x.CreationYear).ToList();
            if (sCars.Count == 0)
            {
                Console.WriteLine("There are no cars available");
            }
            else
            {
                Console.WriteLine("Available cars: ");

                for (int i = 0; i < sCars.Count; i++)
                {
                    Print(sCars[i]);
                }

            }
            Console.WriteLine();
        }

        public void SortByIncreasingYear()
        {
            List<Car> sCars = Cars.OrderBy(x => x.CreationYear).ToList();
            if (sCars.Count == 0)
            {
                Console.WriteLine("There are no cars available");
            }
            else
            {
                Console.WriteLine("Available cars: ");

                for (int i = 0; i < sCars.Count; i++)
                {
                    Print(sCars[i]);
                }

            }
            Console.WriteLine();
        }

        public void ArendCar(int id, Customer customer)
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
                        Console.WriteLine("|---------------------------------------------");
                        Console.WriteLine("Car arended");
                        Console.WriteLine("|---------------------------------------------");
                        break;
                    }
                    else
                    {
                        found = true;
                        Console.WriteLine("|---------------------------------------------");
                        Console.WriteLine("Not enough money to arend car");
                        Console.WriteLine("|---------------------------------------------");
                        break;
                    }
                }
            }
            if (!found)
            {
                Console.WriteLine("|---------------------------------------------");
                Console.WriteLine("Car isn't available");
                Console.WriteLine("|---------------------------------------------");
            }
            Console.WriteLine();
        }

        public void ReturnCar(int id, Customer customer)
        {
            bool found = false;
            for (int i = 0; i < customer.ArendedCars.Count; i++)
            {
                if (customer.ArendedCars[i].Id == id && customer.ArendedCars[i].Arended)
                {
                    Cars.Find(item => item.Id == id).Arended = false;
                    found = true;
                    customer.ArendedCars.Remove(customer.ArendedCars[i]);
                    Console.WriteLine("|---------------------------------------------");
                    Console.WriteLine("Car returned");
                    Console.WriteLine("|---------------------------------------------");
                    break;
                }
            }
            if (!found)
            {

                Console.WriteLine("|---------------------------------------------");
                Console.WriteLine("Car isn't found");
                Console.WriteLine("|---------------------------------------------");
            }
            Console.WriteLine();
        }

        public void BuyCar(int id, Customer customer)
        {
            for (int i = 0; i < Cars.Count; i++)
            {
                if (Cars[i].Id == id && !Cars[i].Arended)
                {
                    if(Cars[i].DiscountPrice != 0)
                    {
                        if (customer.Money < Cars[i].DiscountPrice)
                        {
                            Console.WriteLine("---------------------------------------------");
                            Console.WriteLine("Not enough money");
                            Console.WriteLine("---------------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("|---------------------------------------------");
                            Console.WriteLine("Car bought");
                            Console.WriteLine("|---------------------------------------------");
                            customer.Money -= Cars[i].DiscountPrice;
                            Cars.Remove(Cars[i]);
                        }
                    }
                    else
                    {
                        if (customer.Money < Cars[i].Price)
                        {
                            Console.WriteLine("---------------------------------------------");
                            Console.WriteLine("Not enough money");
                            Console.WriteLine("---------------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("|---------------------------------------------");
                            Console.WriteLine("Car bought");
                            Console.WriteLine("|---------------------------------------------");
                            customer.Money -= Cars[i].Price;
                            Cars.Remove(Cars[i]);
                        }
                    }
                }
            }
            Console.WriteLine();
        }

        public void GetCarsByYear(int year)
        {
            List<Car> CarsWithYear = Cars.FindAll(x => x.CreationYear == year);
            if (CarsWithYear.Count == 0)
            {
                Console.WriteLine("There are no cars available in " + year + " year");
            }
            else
            {
                for (int i = 0; i < CarsWithYear.Count; i++)
                {
                    Print(CarsWithYear[i]);
                }
            }
            Console.WriteLine();
        }

        public void GetCarsByYearRange(int minYear, int maxYear)
        {
            List<Car> CarsWithYear = Cars.FindAll(x => x.CreationYear <= maxYear && x.CreationYear >= minYear);
            if (CarsWithYear.Count == 0)
            {
                Console.WriteLine("|---------------------------------------------");
                Console.WriteLine("There are no cars available between " + minYear + " and " + maxYear + " years");
                Console.WriteLine("|---------------------------------------------");
            }
            else
            {
                Console.WriteLine("Cars between " + minYear + " and " + maxYear + " years:");
                for (int i = 0; i < CarsWithYear.Count; i++)
                {
                    Print(CarsWithYear[i]);
                }
            }
            Console.WriteLine();
        }

        public void GetCarsByPriceRange(int min, int max)
        {

            List<Car> CarsWithPrice = Cars.FindAll(x => x.Price <= max && x.Price >= min);
            Console.WriteLine("Cars with price range " + min + " - " + max + ": ");
            if (CarsWithPrice.Count == 0)
            {
                Console.WriteLine("There are no cars available in range " + min + " and " + max + " dollars");
            }
            else
            {
                for (int i = 0; i < CarsWithPrice.Count; i++)
                {
                    Print(CarsWithPrice[i]);
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
        
        private void Print(Car car)
        {
            Console.WriteLine("|---------------------------------------------");
            Console.WriteLine("|Id: " + car.Id);
            Console.WriteLine("|Name: " + car.CarName);
            Console.WriteLine("|Mark: " + car.CarMark);
            Console.WriteLine("|Creation Year: " + car.CreationYear);
            Console.WriteLine("|Price: " + car.Price);
            Console.WriteLine("|Price with discount: " + (car.DiscountPrice == 0 ? "No discount" : car.DiscountPrice));
            Console.WriteLine("|Available: " + !car.Arended);
            Console.WriteLine("|Arend Price: " + car.ArendPrice);
            Console.WriteLine("|---------------------------------------------");
        }

    }
}