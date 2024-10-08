using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLocalStore
{

    public class Product  //Produkt class som ska lagra vara, pris & antal.
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }

        // Valutakurser
        //public decimal USDConversionRate = 0.11m; // Exempel: 1 SEK = 0.10 USD
        //public decimal GBPConversionRate = 0.095m; // Exempel: 1 SEK = 0.075 GBP

        public Product(string name, decimal price)  // Konstruktor för att skapa produkt med namn och pris.
        {
            Name = name;
            Price = price;
            Amount = 1; // antal produkter börjar med 1.
        }

        // Metod för att konvertera priset till en annan valuta
        public decimal GetPriceInCurrency(string currency)
        {
            decimal USDConversionRate = 0.11m;
            decimal GBPConversionRate = 0.095m;

            switch (currency)
            {
                case "1":
                    return Price * USDConversionRate; // Omvandlar pris till USD
                case "2":
                    return Price * GBPConversionRate; // Omvandlar pris till GBP
                default:
                    return Price; // Returnerar priset i SEK 
            }
        }

        // Shop-metod där användaren kan välja valuta
        public static void Shop(Customer customer)
        {
            List<Product> products = new List<Product> // Skapar en lista över produkter.
            {
                new Product("Korv", 25m),  // produkt med namn och pris att lägga till.
                new Product("Dricka", 15m),
                new Product("Äpple", 5m)
            };

            // Låt användaren välja valuta
            Console.WriteLine("\nVälj i vilken valuta du vill se varorna:\n 1.USD\n 2.GBP\n\nEller valfri knapp för standard valute (SEK) ");
            string selectedCurrency = Console.ReadLine();

            bool shopping = true; // Håller koll på om kunden handlar.

            while (shopping)    // Loop för att hantera shopping tills kunden väljer att avsluta.
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;  // Skriv ut kundens information i blått.
                Console.WriteLine(customer.ToString());
                Console.ResetColor();
                Console.WriteLine("Tillgängliga produkter:\n");

                for (int i = 0; i < products.Count; i++)  // Loopar över tillgängliga produkter och skriver ut dom.
                {
                    decimal priceInSelectedCurrency = products[i].GetPriceInCurrency(selectedCurrency);
                    string currencySymbol;

                    switch (selectedCurrency)
                    {
                        case "1":
                            currencySymbol = "$";
                            break;
                        case "2":
                            currencySymbol = "£";
                            break;
                        default:
                            currencySymbol = "kr";
                            break;
                    }

                    // Visa produkten med pris i vald valuta.
                    Console.WriteLine($"{i + 1}. {products[i].Name} ({priceInSelectedCurrency} {currencySymbol})");
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nKundvagnen innehåller:\n");
                Console.ResetColor();

                customer.ShowCartItems();  // Anropar för att visa kundvagnens innehåll.

                Console.Write("Välj ett alternativ:\n");
                Console.WriteLine("\n1. Lägg till en produkt");
                Console.WriteLine("2. Tillbaka till menyn");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProductToCart(customer, products); // Lägg till produkt i kundvagnen.
                        break;
                    case "2":
                        shopping = false;       // Avsluta shoppingloopen.
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ogiltigt val.");  // Felmeddelande om användaren gör ett ogiltigt val.
                        Console.ResetColor();
                        break;
                }
            }
        }

        // Metod för att lägga till en produkt i kundvagnen
        static void AddProductToCart(Customer customer, List<Product> products)
        {
            Console.Write("Välj en produkt att lägga i kundvagnen (ange nummer): ");

            // Kontrollera att inmatningen är ett giltigt nummer inom produktlistans intervall.
            if (int.TryParse(Console.ReadLine(), out int productChoice) && productChoice > 0 && productChoice <= products.Count)
            {
                // Hämta den valda produkten baserat på användarens val.
                Product selectedProduct = products[productChoice - 1];

                // Lägg till produkten i kundens kundvagn.
                customer.AddToCart(selectedProduct);
                Console.WriteLine($"{selectedProduct.Name} lades till i kundvagnen.");
            }
            else
            {
                // Om användaren gjort ett ogiltigt val, visa ett felmeddelande.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ogiltigt val.");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }

}
