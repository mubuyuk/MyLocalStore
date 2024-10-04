using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLocalStore;


namespace MyLocalStore
{
    internal class Program
    {
        private static List<Customer> customers = new List<Customer>();
        //Customer knatte = new Customer("Knatte", "123");
        //Customer fnatte = new Customer("Fnatte", "321");
        //Customer tjatte = new Customer("Tjatte", "213");

        
       
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                Console.WriteLine("Välkommen till din lokala Mataffär!");
                Console.WriteLine("\n1. Logga in");
                Console.WriteLine("2. Registrera ny användare");
                Console.WriteLine("3. Avsluta");
                Console.WriteLine("\nVälj ett alternativ: ");
                string menuChoise = Console.ReadLine();

                switch (menuChoise)
                {
                    case "1":
                        LoginCustomer();
                        break;
                    case "2":
                        RegisterCustomer();
                        break;
                    case "3":
                        showMenu = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen!");
                        break;

                }
            }


            //Funktion för att registrera en ny kund.
            static void RegisterCustomer()
            {

                Console.Clear();
                Console.WriteLine("Vänligen registrera dig");
                Console.Write("\nVälj ett användarnamn: ");
                string name = Console.ReadLine();
                Console.Write("Välj ett lösenord: ");
                string password = Console.ReadLine();
                
                
                if (customers.Exists(c => c.Name == name)) // Kollar om det redan finns ett inlogg med det namnet.
                {
                    Console.WriteLine("Kunden finns redan registrerad. Försök igen med ett annat namn.");
                }
                else
                {
                    customers.Add(new Customer(name, password));
                    Console.WriteLine("\nNy kund har registrerats! (tryck valfri knapp för att logga in!)");
                    Console.ReadKey();
                    LoginCustomer();
                    return;
                }
            }

            //Funktion för att loggga in kunden.
            static void LoginCustomer()
            {
                Console.Clear();
                Console.WriteLine("Vänligen Logga in ");
                Console.Write("\nAnge användarnamn: ");
                string name = Console.ReadLine();
                Console.Write("Ange lösenord: ");
                string password = Console.ReadLine();
                
                Customer customer = customers.Find(c => c.Name == name); // Kollar om kunden redan är registrerad.

                if (customer == null)
                {
                    Console.WriteLine("Kunden finns inte. Vill du registrera en ny kund? (J/N): ");
                    string answer = Console.ReadLine();
                    if (answer.ToUpper() == "J")
                    {
                        RegisterCustomer();
                    }
                }
                else if (customer.Password != password)
                {
                    Console.WriteLine("Fel lösenord, försök igen eller registrera dig!.\n");
                    Console.WriteLine("1. Försöka igen");
                    Console.WriteLine("2. Registrera dig");
                    string choise = Console.ReadLine();

                    switch (choise)
                    {
                        case "1":
                            LoginCustomer();
                            break;
                        case "2":
                            RegisterCustomer();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"Välkommen {customer.Name}!");
                    ShowCustomerMenu(customer);
                }
            }

            static void ShowCustomerMenu(Customer customer)
            {
                bool loggedIn = true;

                while (loggedIn)
                {
                    Console.Clear();
                    
                    Console.WriteLine($"Välkommen, du är inloggad som {customer.Name}!");
                    Console.WriteLine("\n1. Handla");
                    Console.WriteLine("2. Se kundvagn");
                    Console.WriteLine("3. Gå till kassan");
                    Console.WriteLine("4. Logga ut");
                    Console.Write("\nVälj ett alternativ: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Product.Shop(customer);
                            break;
                        case "2":
                            ShowCart(customer);
                            break;
                        case "3":
                            Checkout(customer);
                            break;
                        case "4":
                            loggedIn = false;
                            break;
                        default:
                            Console.WriteLine("Ogiltigt val, försök igen.");
                            break;
                    }
                }
            }

            //static void Shop(Customer customer)
            //{
            //    List<Product> products = new List<Product>
            //{
            //    new Product("Korv", 25m),
            //    new Product("Dricka", 15m),
            //    new Product("Äpple", 5m)
            //};

            //    bool shopping = true;

            //    while (shopping)
            //    {
            //        Console.Clear();
            //        Console.WriteLine("Tillgängliga produkter:\n");
            //        for (int i = 0; i < products.Count; i++)
            //        {
            //            Console.WriteLine($"{i + 1}. {products[i].Name} ({products[i].Price} kr)");
            //        }

            //        Console.WriteLine("\nKundvagnen innehåller:\n");
            //        if (customer.Cart.Count == 0)
            //        {
            //            Console.WriteLine("Kundvagnen är tom:\n");
            //        }
            //        else
            //        {
            //            foreach (var product in customer.Cart)
            //            {
            //                Console.WriteLine($"{product.Name} - {product.Amount} st, {product.Price} kr/st\n");
            //            }
            //        }

            //        Console.Write("Välj ett alternativ:\n");
            //        Console.WriteLine("\n1. Lägg till en produkt");
            //        Console.WriteLine("2. Tillbaka till menyn");
            //        ;
            //        string choice = Console.ReadLine();

            //        switch (choice)
            //        {
            //            case "1":
            //                AddProductToCart(customer, products);
            //                break;
            //            case "2":
            //                shopping = false;
            //                break;
            //            default:
            //                Console.WriteLine("Ogiltigt val, försök igen.");
            //                break;
            //        }
            //    }
            //}

            //static void AddProductToCart(Customer customer, List<Product> products)
            //{

            //    Console.Write("Välj en produkt att lägga i kundvagnen (ange nummer): ");
            //    if (int.TryParse(Console.ReadLine(), out int productChoice) && productChoice > 0 && productChoice <= products.Count)
            //    {
            //        Product selectedProduct = products[productChoice - 1];
            //        customer.AddToCart(selectedProduct);
            //        Console.WriteLine($"{selectedProduct.Name} lades till i kundvagnen.");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Ogiltigt val.");
            //    }
            //    Console.ReadKey();
            //}


            static void ShowCart(Customer customer)
            {
                Console.WriteLine("Din kundvagn:\n");
                foreach (var product in customer.Cart)
                {
                    Console.WriteLine($"{product.Name} - {product.Amount} st, {product.Price} kr/st");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nTotalpris: {customer.GetCartTotal()} kr");
                Console.ResetColor();
                Console.ReadKey();
            }

            static void Checkout(Customer customer)
            {
                decimal total = customer.GetCartTotal();
                if (total == 0)
                {
                    Console.WriteLine("Din kundvagn är tom.");
                }
                else
                {
                    Console.WriteLine($"Totalt att betala: \n{total} kr. \nTack för ditt köp!");
                    customer.Cart.Clear(); // Töm kundvagnen efter betalning
                    Console.ReadKey();
                }
            }

        }
    }
}

