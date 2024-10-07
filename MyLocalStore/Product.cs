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


        public Product(string name, decimal price)  // Konstruktor för att skapa produkt med namn och pris.
        {
            Name = name;
            Price = price;
            Amount = 1; // antal produkter börjar med 1.
        }

        
        // Shop metod.
        public static void Shop(Customer customer)
        {
            List<Product> products = new List<Product> // skapar en lista över produkter.
            {
                new Product("Korv", 25m),  // produkt med namn och pris att lägga till.
                new Product("Dricka", 15m),
                new Product("Äpple", 5m)
            };

            bool shopping = true; // håller koll på om kunden handlar.

            while (shopping)    // loop för att hantera shopping tills kunden väljer att avsluta
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;  // skriver ut kundens information i blått.
                Console.WriteLine(customer.ToString()); 
                Console.ResetColor();
                Console.WriteLine("Tillgängliga produkter:\n");
                for (int i = 0; i < products.Count; i++)  // loopar över tillgängliga produkter och skriver ut dom.
                {
                    Console.WriteLine($"{i + 1}. {products[i].Name} ({products[i].Price} kr)");
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nKundvagnen innehåller:\n");
                Console.ResetColor();

                customer.ShowCartItems();  // funktionen anropas för att visa kundvagnens innehåll.

                Console.Write("Välj ett alternativ:\n");
                Console.WriteLine("\n1. Lägg till en produkt");
                Console.WriteLine("2. Tillbaka till menyn");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProductToCart(customer, products); // lägger till produkt i kundvagnen.
                        break;
                    case "2":
                        shopping = false;       // avslutar shopping loopen.
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ogiltigt val.");  // felmeddelande om användaren gör ett ogiltigt val
                        Console.ResetColor();
                        //Console.ReadKey();
                        break;
                }       //Console.ReadKey();
            } //Console.ReadKey();
        }

        
        // Metod för att lägga till en produkt i kundvagnen
        static void AddProductToCart(Customer customer, List<Product> products)
        {
            
            Console.Write("Välj en produkt att lägga i kundvagnen (ange nummer): ");

            // Kontrollera att inmatningen är ett giltigt nummer inom produktlistans intervall
            if (int.TryParse(Console.ReadLine(), out int productChoice) && productChoice > 0 && productChoice <= products.Count)
            {
                // Hämta den valda produkten baserat på användarens val
                Product selectedProduct = products[productChoice - 1];

                // Lägg till produkten i kundens kundvagn
                customer.AddToCart(selectedProduct);
                Console.WriteLine($"{selectedProduct.Name} lades till i kundvagnen.");
            }
            else
            {
                // Om användaren gjort ett ogiltigt val, visa ett felmeddelande
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ogiltigt val.");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}
