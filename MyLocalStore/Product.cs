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

        public Product(string name, decimal price)  // Konstruktor
        {
            Name = name;
            Price = price;
            Amount = 1;
        }

        public static void Shop(Customer customer)
        {
            List<Product> products = new List<Product>
            {
                new Product("Korv", 25m),
                new Product("Dricka", 15m),
                new Product("Äpple", 5m)
            };

            bool shopping = true;

            while (shopping)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(customer.ToString());
                Console.ResetColor();
                Console.WriteLine("Tillgängliga produkter:\n");
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {products[i].Name} ({products[i].Price} kr)");
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nKundvagnen innehåller:\n");
                Console.ResetColor();

                customer.ShowCartItems();  // funktionen för att visa kundvagnen

                Console.Write("Välj ett alternativ:\n");
                Console.WriteLine("\n1. Lägg till en produkt");
                Console.WriteLine("2. Tillbaka till menyn");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProductToCart(customer, products);
                        break;
                    case "2":
                        shopping = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ogiltigt val.");
                        Console.ResetColor();
                        //Console.ReadKey();
                        break;
                }       //Console.ReadKey();
            } //Console.ReadKey();
        }

        static void AddProductToCart(Customer customer, List<Product> products)
        {
            
            Console.Write("Välj en produkt att lägga i kundvagnen (ange nummer): ");
            if (int.TryParse(Console.ReadLine(), out int productChoice) && productChoice > 0 && productChoice <= products.Count)
            {
                Product selectedProduct = products[productChoice - 1];
                customer.AddToCart(selectedProduct);
                Console.WriteLine($"{selectedProduct.Name} lades till i kundvagnen.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ogiltigt val.");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}
