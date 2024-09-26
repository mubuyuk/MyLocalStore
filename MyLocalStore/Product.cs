using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLocalStore
{
    internal class Product  //Produkt class som ska lagra vara, pris & antal.
    {
        public string Item { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }

        public Product(string item, double price, int amount)  // Konstruktor
        {
            Item = item;
            Price = price;
            Amount = 1;
        }
    }

    //public bool DiscountLevel() // giraffe academy Object Methods | C# | Tutorial 27
    //{
    //    if ()
    //    {
    //        //implementera 3 nivåer av kund
    //        return true;
    //    } 
    //    return false;
    //}


}
