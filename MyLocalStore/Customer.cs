﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalStore
{
    public class Customer //Kund class som ska lagra namn, lösenord & kundvagn.
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        private List<Product> _cart;
        public List<Product> Cart { get { return _cart;} }



        // Konstruktor för att skapa ett Customer-objekt och tilldela initiala värden.
        public Customer(string name, string password)
        {
            Name = name;                // Tilldelar parametern 'name' till egenskapen 'Name'
            Password = password;
            _cart = new List<Product>(); // Initierar en ny tom lista för kundvagnen.
            
        }

        public void AddToCart(Product product)
        {
            // kontrollerar om produkten redan finns i kundvagnen.
            var choosenProduct = Cart.Find(p => p.Name == product.Name);
            if (choosenProduct != null)
            {
                // om produkten finns, öka antal
                choosenProduct.Amount++;
            }
            // om produkten inte finns, lägg till den.
            else
            {
                Cart.Add(product);
            }
        }

        // Metod för att visa alla produkter i kundvagnen
        public void ShowCartItems()
        {
            // om kundvagnen är tom, visa detta meddelande.
            if (Cart.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Din kundvagn är tom.");
                Console.ResetColor();
            }
            else
            {
                // loopar igenom varje produkt i kundvagnene och skriver ut namn, antal, pris.
                foreach (var product in Cart)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{product.Name} - {product.Amount} st, {product.Price} kr/st");
                    Console.ResetColor();
                }
                //beräknar och skriver ut totala priset för alla varror i kundvagnen.
                decimal total = GetCartTotal();
                
                Console.WriteLine($"\nTotalpris: {total} kr");
                
            }
        }

        public decimal GetCartTotal()
        {
            decimal total = 0; // variabel för att lagra totalpriset.

            // loopar igenom varje produkt i kundvagnen och multiplicerar priset med mängden.
            foreach (var product in Cart)
            {
                total += product.Price * product.Amount; // adderar varje produkts total pris till total.
            }
            // returnerar totalpriset.
            return total;
        }

        // Virtuell metod för att beräkna rabatt. Den kommer att överskrivas i subklasser.
        public virtual decimal CalculateDiscount(decimal total)
        {
            // Basklassen Customer har ingen specifik rabatt
            return 0;
        }

        public override string ToString()
        {
            return $"Kund: {Name}, Lösenord: {Password}";
        }
    }
}
