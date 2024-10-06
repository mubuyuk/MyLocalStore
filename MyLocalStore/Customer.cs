﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalStore
{
    public class Customer //Kund class som ska lagra namn, lösenord & kundkorg.
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        private List<Product> _cart;
        public List<Product> Cart { get { return _cart;} }
        public string CustomerLevel { get; set; }


        // Constructor med  parametrar, har samma naman som classen, har ingen returtyp
        public Customer(string name, string password)
        {
            Name = name;                // Tilldela parametrarna till fällten (Fields)
            Password = password;
            _cart = new List<Product>();
        }

        public void AddToCart(Product product)
        {
            var choosenProduct = Cart.Find(p => p.Name == product.Name);
            if (choosenProduct != null)
            {
                choosenProduct.Amount++;
            }
            else
            {
                Cart.Add(product);
            }
        }

        // Metod för att visa kundvagnen
        public void ShowCartItems()
        {
            if (Cart.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Din kundvagn är tom.");
                Console.ResetColor();
            }
            else
            {
                foreach (var product in Cart)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{product.Name} - {product.Amount} st, {product.Price} kr/st");
                    Console.ResetColor();
                }
                decimal total = GetCartTotal();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nTotalpris: {total} kr");
                Console.ResetColor();
            }
        }

        public decimal GetCartTotal()
        {
            decimal total = 0;
            foreach (var product in Cart)
            {
                total += product.Price * product.Amount;
            }

            return total;
        }

        public override string ToString()
        {
            return $"Kund: {Name}, Lösenord: {Password}";
        }
    }
}
