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

        public Dictionary<string, List<Car>> BoughtCars { get; private set; }

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
            if (!File.Exists("Checks.json"))
            {
                File.Create("Checks.json").Close();
                BoughtCars = new Dictionary<string, List<Car>> { };
            }
            else
            {
                string json = File.ReadAllText("Checks.json");
                BoughtCars = JsonConvert.DeserializeObject<Dictionary<string, List<Car>>>(json);
                if (BoughtCars == null)
                {
                    BoughtCars = new Dictionary<string, List<Car>> { };
                }
            }

            for (int i = 0; i < Cars.Count; i++)
            {
                if (Cars[i].Arended && Cars[i].EndArendDate <= DateTime.Now)
                {
                    Cars[i].Arended = false;
                }
            }
            for (int i = 0; i < Customers.Count; i++)
            {
                for (int j = 0; j < Customers[i].ArendedCars.Count; j++)
                {
                    if (Customers[i].ArendedCars[j].EndArendDate <= DateTime.Now)
                    {
                        Customers[i].ArendedCars.Remove(Customers[i].ArendedCars[j]);
                    }
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
            Console.Write("Please enter car arend price per day: ");
            double arendPrice = Validator.DoubleValidation();
            Console.Write("Please enter car with discount price if any discount present else write 0: ");
            double discountPrice = Validator.DoubleValidation();
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

        public void ArendCar(Customer customer)
        {
            Console.Write("Please enter car id: ");
            string id = Validator.StringValidation();
            Console.Write("Please amount of days you want arend a car: ");
            int days = Validator.IntValidation();
            bool found = false;

            for (int i = 0; i < Cars.Count; i++)
            {
                if (Cars[i].Id == id &&!Cars[i].Arended)
                {
                    if(customer.Money >= Cars[i].ArendPrice * days)
                    {
                        customer.Money -= Cars[i].ArendPrice * days;
                        Cars[i].ArendCar(days);
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

        public void ReturnCar(Customer customer)
        {
            Console.Write("Please enter car id: ");
            string id = Validator.StringValidation();
            bool found = false;
            DateTime time = DateTime.Now;
            for (int i = 0; i < customer.ArendedCars.Count; i++)
            {
                if (customer.ArendedCars[i].Id == id && customer.ArendedCars[i].Arended)
                {
                    if(customer.ArendedCars[i].EndArendDate > time)
                    {
                        ReturnBalanceToCustomer(customer, (customer.ArendedCars[i].EndArendDate.Value.Day - time.Day) * customer.ArendedCars[i].ArendPrice);
                        Car car = Cars.Find(item => item.Id == id);
                        car.Arended = false;
                        car.StartArendDate = null;
                        car.EndArendDate = null;
                        found = true;
                        customer.ArendedCars.Remove(customer.ArendedCars[i]);
                        Log.Success("Car returned");
                        break;
                    }
                }
            }
            if (!found)
            {
                Log.Warning("Car isn't found");
            }
            Console.WriteLine();
        }

        public void BuyCar(Customer customer)
        {
            Console.Write("Please enter car id: ");
            string id = Validator.StringValidation();
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
                            customer.Money -= Cars[i].DiscountPrice;
                            CreateCheck(customer, Cars[i]);
                            Cars.Remove(Cars[i]);
                            Log.Success("Car bought");
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
                            customer.Money -= Cars[i].Price;
                            CreateCheck(customer, Cars[i]);
                            Cars.Remove(Cars[i]);
                            Log.Success("Car bought");
                            return;
                        }
                    }
                }
            }
            Log.Warning("Car not found");
            Console.WriteLine();
        }

        public void GetCarsByYear()
        {
            Console.WriteLine("Please enter the year of creation");
            int year = Validator.IntValidation();
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

        public void GetCarsByYearRange()
        {
            Console.Write("Please enter the min year of creation: ");
            int min_year = Validator.IntValidation();
            Console.Write("Please enter the max year of creation: ");
            int max_year = Validator.IntValidation();

            int min = min_year > max_year ? max_year : min_year;
            int max = min_year > max_year ? min_year : max_year;
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

        public void GetCarsByPriceRange()
        {
            Console.Write("Please enter the min price: ");
            double min_price = Validator.DoubleValidation();
            Console.Write("Please enter the max price (111,5): ");
            double max_price = Validator.DoubleValidation();

            double minPrice = min_price > max_price ? max_price : min_price;
            double maxPrice = min_price > max_price ? min_price : max_price;
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

        public void GetAllArendedCars()
        {
            bool found = false;
            for(int i = 0; i < Customers.Count; i++)
            {
                if (Customers[i].ArendedCars.Count != 0)
                {
                    for (int j = 0; j < Customers[i].ArendedCars.Count; j++)
                    {
                        found = true;
                        Log.Warning($"|Customer: {Customers[i].Username}\n{ Customers[i].ArendedCars[j]}\n|{Customers[i].ArendedCars[j].StartArendDate} - {Customers[i].ArendedCars[j].EndArendDate}");
                    }
                }
            }
            if (!found)
            {
                Log.Error("There are no arended cars");
            }
        }

        public void GetCustomerBoughtCars()
        {
            Console.Write("Please enter customer's name: ");
            string name = Validator.StringValidation();
            int index = Customers.FindIndex(x => x.Username == name);
            bool found = false;
            if (index >= 0)
            {
                foreach (KeyValuePair<string, List<Car>> entry in BoughtCars)
                {
                    if(entry.Key == name)
                    {
                        foreach (Car car in entry.Value)
                        {
                            Log.Warning($"|Customer: {entry.Key}\n{car}");
                        }
                        found = true;
                    }
                }
            }
            else
            {
                Log.Error("Customer does not exists");
            }
            if (!found)
            {
                Log.Warning("Customer hasn't bought anything");
            }
        }

        public void GetCustomersArendedCars()
        {
            Console.Write("Please enter customer's name: ");
            string name = Validator.StringValidation();
            int index = Customers.FindIndex(x => x.Username == name);
            if (index >= 0)
            {
                if(Customers[index].ArendedCars.Count == 0)
                {
                    Log.Warning("That customer doesn't has arended cars");
                }
                else
                {
                    for (int i = 0; i < Customers[index].ArendedCars.Count; i++)
                    {
                        Log.Warning($"|Customer: {Customers[index].Username}\n{Customers[index].ArendedCars[i]}");
                    }
                }
            }
            else
            {
                Log.Error("Custopmer does not exists");
            }

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
            string json = JsonConvert.SerializeObject(Cars, Formatting.Indented);
            File.WriteAllText("data.json", json);
            string customers = JsonConvert.SerializeObject(Customers, Formatting.Indented);
            File.WriteAllText("customers.json", customers);
            string checks = JsonConvert.SerializeObject(BoughtCars, Formatting.Indented);
            File.WriteAllText("Checks.json", checks);
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

        public void AddBalanceToCustomer()
        {
            Console.Write("Please enter customer's name: ");
            string name = Validator.StringValidation();
            int index = Customers.FindIndex(x => x.Username == name);
            if (index >= 0)
            {
                Console.Write("Please enter money amount: ");
                double money = Validator.DoubleValidation();
                Customers[index].AddBalance(money);
                Log.Success("Money were added to customer");
            }
            else
            {
                Log.Warning("Customer does not exists");
            }

        }

        private void ReturnBalanceToCustomer(Customer activeCustomer, double sum)
        {
            activeCustomer.AddBalance(sum);
        }

        public void ChangeCarInfo()
        {
            Console.Write("Write car's id that you want to change: ");
            string id = Validator.StringValidation();

            for (int i = 0; i < Cars.Count; i++)
            {
                if (Cars[i].Id == id && !Cars[i].Arended)
                {

                    Cars[i].ChangeInfo();
                    Log.Success("Car has been changed");
                    return;
                }
                else if(Cars[i].Id == id && Cars[i].Arended)
                {
                    Log.Warning("Car arended can't change info");
                    return;
                }
            }

            Log.Warning("Car not found");
            Console.WriteLine();
        }

        public void GetBoughtCars()
        {
            bool found = false;

            foreach (KeyValuePair<string, List<Car>> entry in BoughtCars)
            { 
                foreach(Car car in entry.Value)
                {
                    Log.Warning($"|Customer: {entry.Key}\n{car}");
                    found = true;
                }
            }
            if (!found)
            {
                Log.Error("Nobody has'nt bought car yet");
            }
        }

        public void RemoveCar()
        {
            
            Console.Write("Please enter car id: ");
            string id = Validator.StringValidation();
            bool found = false;

            for (int i = 0; i < Cars.Count; i++)
            {
                if (Cars[i].Id == id && !Cars[i].Arended)
                {
                    found = true;
                    Cars.Remove(Cars[i]);
                    Log.Success("Car deleted");
                }
                else if (Cars[i].Id == id && Cars[i].Arended)
                {
                    found = true;
                    Log.Error("Can't delete car, car arended");
                }
            }
            if (!found)
            {
                Log.Error("Car not found");
            }
            Console.WriteLine();
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

        private void CreateCheck(Customer customer, Car car)
        {
            car.boughtDate = DateTime.Today;
            if (BoughtCars.ContainsKey(customer.Username))
            {
                BoughtCars[customer.Username].Add(car);
            }
            else
            {
                BoughtCars.Add(customer.Username, new List<Car> { car });
            }
            
        }

    }
}