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
                Console.WriteLine("Tillgängliga produkter:\n");
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {products[i].Name} ({products[i].Price} kr)");
                }

                Console.WriteLine("\nKundvagnen innehåller:\n");
                if (customer.Cart.Count == 0)
                {
                    Console.WriteLine("Kundvagnen är tom:\n");
                }
                else
                {
                    foreach (var product in customer.Cart)
                    {
                        Console.WriteLine($"{product.Name} - {product.Amount} st, {product.Price} kr/st\n");
                    }
                }

                Console.Write("Välj ett alternativ:\n");
                Console.WriteLine("\n1. Lägg till en produkt");
                Console.WriteLine("2. Tillbaka till menyn");
                ;
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
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
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
                Console.WriteLine("Ogiltigt val.");
            }
            Console.ReadKey();
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
