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
        public DateTime? boughtDate { get; set; }
        public DateTime? StartArendDate { get; set; }
        public DateTime? EndArendDate { get; set; }

        public Car(string id, int year, double price, string name, double discount, double arendPrice, string mark)
        {
            CreationYear = year;
            Price = price;
            CarName = name;
            DiscountPrice = discount;
            ArendPrice = arendPrice;
            CarMark = mark;
            Id = id;
            boughtDate = null;
            StartArendDate = null;
        }

        public void ChangeInfo()
        {
            Console.Write("Write new car's creation year or 0: ");
            int creationYear = Validator.IntValidation();
            Console.Write("Write new car's price or 0: ");
            double price = Validator.DoubleValidation();
            Console.Write("Write new car's name or 0: ");
            string carName = Validator.StringValidation();
            Console.Write("Write new car's mark or 0: ");
            string mark = Validator.StringValidation();
            Console.Write("Write new car's arend price or 0: ");
            double arendPrice = Validator.DoubleValidation();
            Console.Write("Write new car's discount price price if there no discount write 0 or -1: ");
            double discountPrice = Validator.DoubleChangeValidation();

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
            else if (creationYear < 1886 && creationYear != 0)
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
                CreationYear = creationYear == 0? CreationYear : creationYear;
                Price = price == 0 ? Price : price;
                CarName = carName == "0" ? CarName : carName;
                DiscountPrice = discountPrice == -1 ? DiscountPrice : discountPrice;
                ArendPrice = arendPrice == 0 ? ArendPrice : arendPrice;
                CarMark = mark == "0" ? CarMark : mark;
            }  
        }

        public void ArendCar(int days) 
        {
            Arended = true;
            StartArendDate = DateTime.Now;
            EndArendDate = StartArendDate.Value.AddDays(days);
        }

        public override string ToString()
        {
            return $"|Id: { Id }\n" +
            $"|Name: { CarName }\n" +
            $"|Mark: { CarMark }\n" +
            $"|Creation Year: { CreationYear }\n" +
            $"|Price: {Price }\n" +
            $"|Price with discount: {(DiscountPrice == 0 ? "No discount" : DiscountPrice)}\n" +
            $"{(boughtDate == null ? "" : "|Bought date:" + boughtDate + "\n")}"+
            $"|Arend Price: { ArendPrice }" ;
        }

    }
}
