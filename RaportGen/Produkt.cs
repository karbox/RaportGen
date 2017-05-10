using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaportGen
{
    public class Produkt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Ilosc { get; set; }
        public Produkt(string name, double price)//, int ilosc)
        {
            Name = name;
            Price = price;
            // Ilosc = ilosc;

        }
        public Produkt() { }
        public string Sstring()
        {
            return Name + " " + Price + " zł";
        }
    }
}
