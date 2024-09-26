using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLocalStore;


namespace MyLocalStore

{
    internal class Program
    {
        static void Main(string[] args)
        {

            MainMenu();


        }

        public static void MainMenu()
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
                        LoginCustomer(userName, userPassword);
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

        }

        static string userName;
        static string userPassword;

        public static bool LoginCustomer(string username, string password) 
        {
            Console.WriteLine("Logga in ");
            Console.Write("Ange användarnamn: ");
            string userName = Console.ReadLine();

            Console.Write("Ange lösenord: ");
            string userPassword = Console.ReadLine();

            if (userName == username && userPassword == password)
            {
               return true;
            }
            else   
            {
                Console.WriteLine("\nAnvändare finns ej, registrera ny användare");
                RegisterCustomer();
                return false;
            }

            
        }

        public static void RegisterCustomer()
        {
            Console.WriteLine("Vänligen registrera dig");
            Console.Write("\nVälj ett användarnamn: ");

            string userName = Console.ReadLine();
            Console.Write("Välj ett lösenord: ");
            string userPassword = Console.ReadLine();

            Console.Clear();
            Customer customer1 = new Customer(userName, userPassword);
            Console.WriteLine("Ny kund registrerad, Välkommen" + " " + customer1.Name);

            if (LoginCustomer(userName, userPassword))
            {
                Console.WriteLine("Inloggning lyckades");

            }
            else 
            {
                Console.WriteLine("Felaktigt anv. eller lösen.");
                RegisterCustomer();
            }            
                Console.ReadKey();
        }
    }
}
