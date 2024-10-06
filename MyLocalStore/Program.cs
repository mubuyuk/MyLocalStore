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

        static void Main(string[] args)
        {

            // 3 st föridentifierade kunder att logga in med.
            customers.Add(new Customer("Knatte", "123"));
            customers.Add(new Customer("Fnatte", "321"));
            customers.Add(new Customer("Tjatte", "213"));

            // Huvudmeny för Affären.
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
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ogiltigt val.");
                        Console.ResetColor();
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
                    customers.Add(new Customer(name, password)); // Skapar ny kund med valt namn och lösenord.
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
                
                Customer customer = customers.Find(c => c.Name == name); // Kollar om kunden är registrerad.

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
                    Console.WriteLine($"Välkommen {customer.ToString}!");
                    ShowCustomerMenu(customer);
                }
            }
            // Funktionene för menyn efter inlogg.
            static void ShowCustomerMenu(Customer customer)
            {
                bool loggedIn = true;

                while (loggedIn)
                {
                    Console.Clear();
                    Console.WriteLine("Välkommen");
                    Console.WriteLine(customer.ToString());
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
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ogiltigt val.");
                            Console.ResetColor();
                            break;
                    }
                    Console.ReadKey();
                }
            }

            static void ShowCart(Customer customer)
            {
                Console.Clear();  // Rensar konsolen för att hålla det snyggt
                Console.WriteLine(customer.ToString());  // Visa kundens namn och lösenord
                Console.WriteLine("Din kundvagn:\n");

                customer.ShowCartItems();  // Använder funktionen för att visa kundvagnen

                Console.WriteLine("Tryck valfri knapp för att gå tillbaka...");
                
            }

            // Funktion för att avsluta köpet.
            static void Checkout(Customer customer)
            {
                decimal total = customer.GetCartTotal();
                if (total == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Din kundvagn är tom.");
                    Console.ResetColor();
                }
                else
                {   Console.Clear();

                    //Console.WriteLine($"Kundnivå: {customer.CustomerLevel}"); // Visa kundnivån
                    Console.WriteLine("Din kund korg innehåller: ");
                    customer.ShowCartItems();
                    Console.WriteLine("Tack för ditt köp!");
                    customer.Cart.Clear(); // Töm kundvagnen efter betalning
                    Console.WriteLine("Tryck valfi knapp för att återgå till menyn");
                    Console.ReadKey();
                }
            }
        }
    }
}

