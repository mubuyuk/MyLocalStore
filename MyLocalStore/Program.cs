﻿using System;
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
                        showMenu = false;
                        break;
                    default:
                        
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ogiltigt val.");
                        Console.ResetColor();
                        Console.ReadKey();
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
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(customer.ToString());  // Visa kundens inloggningsuppgifter.
                Console.ResetColor();                       
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
                {
                    Console.Clear();
                    string customerLevel = customer.GetCustomerLevel(total); //hämtar nivån (Gold, Silver, Bronze) baserat på totalen för köpet.
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(customer.ToString());
                    Console.ResetColor();

                    Console.WriteLine("Din Kundvagn: ");
                    decimal discount = customer.CalculateDiscount(total); // Beräkna rabatt baserat på kundnivå
                    decimal totalAfterDiscount = total - discount;

                    customer.ShowCartItems();
                    Console.WriteLine($"Kundnivå: {customerLevel}"); // skriver ut vilken nivå kunden befinner sig på i samband med köpet.
                    Console.WriteLine($"Rabatt: {discount} kr");
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine($"Totalpris efter rabatt: {totalAfterDiscount} kr");
                    Console.ResetColor();
                    
                    customer.Cart.Clear(); // Töm kundvagnen efter betalning
                    Console.WriteLine("\nTack för ditt köp!");
                    Console.ReadKey();
                }
            }
        }
    }
}

