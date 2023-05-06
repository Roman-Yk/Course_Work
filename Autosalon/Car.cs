using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autosalon
{
    class Car
    {
        public int CreationYear { get; set; }
        public double Price { get; set; }
        public string CarName{ get; set; }
        public string CarMark { get; set; }
        public double DiscountPrice { get; set; }
        public bool Arended { get; set; }
        public double ArendPrice { get; set; }
        public string Id { get; set; }

        public Car(string id, int year, double price, string name, double discount, double arendPrice, string mark)
        {
            CreationYear = year;
            Price = price;
            CarName = name;
            DiscountPrice = discount;
            ArendPrice = arendPrice;
            CarMark = mark;
            Id = id;
        }

        public void ChangeInfo()
        {
            Console.Write("Write car's creation year: ");
            int creationYear = Validator.IntValidation();
            Console.Write("Write car's price: ");
            double price = Validator.DoubleValidation();
            Console.Write("Write car's name: ");
            string carName = Validator.StringValidation();
            Console.Write("Write car's mark: ");
            string mark = Validator.StringValidation();
            Console.Write("Write car's arend price: ");
            double arendPrice = Validator.DoubleValidation();
            Console.Write("Write car's discount price price if there no discount write 0: ");
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
            else if (creationYear < 1886)
            {
                Console.WriteLine();
                Log.Warning("The first car was created in 1886");
                Console.WriteLine();
            }
            else if (creationYear > 2023)
            {
                Console.WriteLine();
                Log.Warning("Are you from future?");
                Console.WriteLine();
            }
            else
            {
                CreationYear = creationYear;
                Price = price;
                CarName = carName;
                DiscountPrice = discountPrice;
                ArendPrice = arendPrice;
                CarMark = mark;
            }  
        }

        public override string ToString()
        {
            return $"|Id: { Id }\n" +
            $"|Name: { CarName }\n" +
            $"|Mark: { CarMark}\n" +
            $"|Creation Year: { CreationYear}\n" +
            $"|Price: {Price}\n" +
            $"|Price with discount: {(DiscountPrice == 0 ? "No discount" : DiscountPrice)}\n" +
            $"|Arend Price: { ArendPrice}" ;
        }

    }
}
