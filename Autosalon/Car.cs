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
        public int Id { get; set; }

        public Car()
        {

        }

        public Car(int id, int year, double price, string name, double discount, double arendPrice, string mark)
        {
            this.CreationYear = year;
            this.Price = price;
            this.CarName = name;
            this.DiscountPrice = discount;
            this.ArendPrice = arendPrice;
            this.CarMark = mark;
            this.Id = id;
        }
        
    }
}
