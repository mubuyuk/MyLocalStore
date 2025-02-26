﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalStore
{
    public class DiscountLevels
    {
        public class GoldCustomer : Customer // Subklass som ärver från basklassen Customer
        {
            // Konstruktor för GoldCustomer som tar in namn och lösenord, och skickar vidare dessa till basklassen (Customer)
            public GoldCustomer(string name, string password) : base(name, password)
            {
            }

            // Överskrid metoden för att ge 15% rabatt
            public override decimal CalculateDiscount(decimal total)
            {
                return total * 0.15m;
            }

            public override string ToString()
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                return $"Kund: {Name} Kundnivå: [Gold] 15% rabatt!";
                
            }
        }

        public class SilverCustomer : Customer
        {
            public SilverCustomer(string name, string password) : base(name, password)
            {
            }

            // Överskrid metoden för att ge 10% rabatt
            public override decimal CalculateDiscount(decimal total)
            {
                return total * 0.10m;
            }

            public override string ToString()
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                return $"Kund: {Name} Kundnivå: [Silver] 10% rabatt!";
            }
        }

        public class BronzeCustomer : Customer
        {
            public BronzeCustomer(string name, string password) : base(name, password)
            {
            }

            // Överskrid metoden för att ge 5% rabatt
            public override decimal CalculateDiscount(decimal total)
            {
                return total * 0.05m;
            }

            public override string ToString()
            {
                
                return $"Kund: {Name} Kundnivå: [Bronze] 5% rabatt!";
            }
        }
    }
}

