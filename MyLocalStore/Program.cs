using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLocalStore;
using static MyLocalStore.DiscountLevels;


namespace MyLocalStore
{
    internal class Program
    {
        // Lista över registrerade kunder.
        private static List<Customer> customers = new List<Customer>();
        private static string filePath = "customers.txt";  // Filens sökväg till textfilen där kunderna sparas.
        
        static void Main(string[] args)
        {
            // Ladda in kunder från filen vid programmets start
            LoadCustomersFromFile(filePath);

            // 3 st föridentifierade kunder att logga in med
            customers.Add(new GoldCustomer("Knatte", "123"));   // Gold-kund (15% rabatt)
            customers.Add(new SilverCustomer("Fnatte", "321")); // Silver-kund (10% rabatt)
            customers.Add(new BronzeCustomer("Tjatte", "213")); // Bronze-kund (5% rabatt)

            // Huvudmeny för Affären.
            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
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
                        // Spara kunder till filen när programmet avslutas
                        SaveCustomersToFile(filePath);
                        showMenu = false;
                        break;
                    default:
                        // Anropa hjälpfunktionen vid ogiltig inmatning
                        Helper.ShowInvalidInputMessage();
                        Console.ReadKey();
                        break;
                        
                }
            }

            // Funktionen för att spara alla kunder till textfil
            static void SaveCustomersToFile(string filePath)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Loopar genom alla kunder.
                    foreach (var customer in customers)
                    {
                        // Sparar varje kund med: Namn, Lösenord, Kundtyp
                        writer.WriteLine($"{customer.Name},{customer.Password},{customer.GetType().Name}");
                    }
                }
            }

            // Funktionen för att läsa in alla kunder från textfil
            static void LoadCustomersFromFile(string filePath)
            {
                if (File.Exists(filePath))  // Kontrollera att filen finns
                {
                    // Använder en StreamWriter för att skriva till filen
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Läser varje rad från textfilen och skapar kunder baserat på data
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] data = line.Split(',');  // Dela upp varje rad med komma
                            string name = data[0];
                            string password = data[1];
                            string type = data[2];

                            // Skapa rätt kundtyp beroende på vad som står i filen (Gold, Silver, Bronze)
                            Customer customer;
                            switch (type)
                            {
                                case "GoldCustomer":
                                    customer = new GoldCustomer(name, password);
                                    break;
                                case "SilverCustomer":
                                    customer = new SilverCustomer(name, password);
                                    break;
                                case "BronzeCustomer":
                                    customer = new BronzeCustomer(name, password);
                                    break;
                                default:
                                    customer = new Customer(name, password);  // Standardkund om något går fel
                                    break;
                            }
                            customers.Add(customer);  // Lägg till kunden i listan
                        }
                    }
                }
            }

            // Funktion för att registrera en ny kund
            static void RegisterCustomer()
            {
                Console.Clear();
                Console.WriteLine("Vänligen registrera dig");

                string name;
                string password;

                // Kontrollera att användarnamnet inte är tomt och inte redan existerar.
                do
                {
                    Console.Write("\nVälj ett användarnamn: ");
                    name = Console.ReadLine();

                    // Kontrollera om användarnamnet är tomt.
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Användarnamn kan inte vara tomt. Försök igen.");
                        Console.ResetColor();
                        
                    }
                    // Kontrollera om användarnamnet redan existerar.
                    else if (customers.Exists(c => c.Name == name))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Användarnamnet är redan registrerat. Försök igen med ett annat namn.");
                        Console.ResetColor();
                        name = ""; // Återställ namnet för att fortsätta loopen.
                    }
                } while (string.IsNullOrWhiteSpace(name));  // Fortsätt fråga tills ett giltigt och unikt namn anges.

                // Kontrollera att lösenordet inte är tomt.
                do
                {
                    Console.Write("Välj ett lösenord: ");
                    password = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(password))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lösenord kan inte vara tomt. Försök igen.");
                        Console.ResetColor();
                        
                    }
                } while (string.IsNullOrWhiteSpace(password));  // Fortsätt fråga tills ett giltigt lösenord anges.

                // Låt användaren välja kundnivå (Gold, Silver, Bronze)
                Console.WriteLine("Välj kundnivå (1: Gold, 2: Silver, 3: Bronze): ");
                string levelChoice = Console.ReadLine();
                Customer newCustomer;

                // Skapar rätt kundtyp beroende på användarens val
                switch (levelChoice)
                {
                    case "1":
                        newCustomer = new GoldCustomer(name, password);
                        break;
                    case "2":
                        newCustomer = new SilverCustomer(name, password);
                        break;
                    case "3":
                        newCustomer = new BronzeCustomer(name, password);
                        break;
                    default:
                        newCustomer = new Customer(name, password);  // Default om valet inte är korrekt
                        break;
                }

                customers.Add(newCustomer);  // Lägg till den nya kunden i listan
                Console.WriteLine("\nNy kund har registrerats!");

                // Spara alla kunder till textfilen efter registrering
                SaveCustomersToFile(filePath);
                Console.ReadKey();
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
                    // Om kunden inte hittas, fråga om användaren vill registrera sig
                    Console.WriteLine("Kunden finns inte. Vill du registrera en ny kund? (J/N): ");
                    string answer = Console.ReadLine();
                    if (answer.ToUpperInvariant() == "J")
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

                    // Hantera användarens val vid felaktigt lösenord
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
                    ShowCustomerMenu(customer); // Skickar kunden vidare till menyn efter lyckad inlogg.
                }
            }
            // Funktionene för menyn efter inlogg.
            static void ShowCustomerMenu(Customer customer)
            {
                bool loggedIn = true;

                while (loggedIn)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(customer.ToString());
                    Console.ResetColor();
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
                            // Anropa hjälpfunktionen vid ogiltig inmatning
                            Helper.ShowInvalidInputMessage();
                            break;
                            
                    }
                    Console.ReadKey();
                }
            }

            // Funktion för att visa kundvagnen.
            static void ShowCart(Customer customer)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(customer.ToString());
                Console.ResetColor();
                Console.WriteLine("Din kundvagn:\n");

                customer.ShowCartItems(); // metod som visar alla produkter i kundvagnen.

                Console.WriteLine("Tryck valfri knapp för att gå tillbaka...");
            }

            // Funktion för att avsluta köpet.
            static void Checkout(Customer customer)
            {
                //Hämtar totalbelopp från kundens kundvagn.
                decimal total = customer.GetCartTotal();
                if (total == 0)
                {
                    // Meddelande om kundvagnene är tom.
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Din kundvagn är tom.");
                    Console.ResetColor();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(customer.ToString());  // visar info om kunden
                    Console.ResetColor();

                    Console.WriteLine("Din Kundvagn: ");
                    customer.ShowCartItems(); // metod som visar alla produkter i kundvagnen

                    // Räknar ut och visar rabatt.
                    decimal discount = customer.CalculateDiscount(total); // Använder den fasta rabatten för varje kundtyp
                    decimal totalAfterDiscount = total - discount;

                    Console.WriteLine($"Rabatt: {discount} kr");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Totalpris efter rabatt: {totalAfterDiscount} kr");
                    Console.ResetColor();

                    //Tömmer kundvagnene efter köp.
                    customer.Cart.Clear();
                    Console.WriteLine("\nTack för ditt köp!");
                    Console.WriteLine("Tryck valfri knapp för att återgå..");
                    Console.ReadKey();
                }
            }
        }
    }
}

