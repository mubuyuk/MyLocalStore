using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalStore
{
    internal class Helper
    {
        // Metod för att visa felmeddelande vid ogiltig inmatning
        public static void ShowInvalidInputMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;  // Sätter textfärgen till röd
            Console.WriteLine("Ogiltigt val, försök igen.");  // Visar ett felmeddelande
            Console.ResetColor();  // Återställer färgen till standardfärgen
        }
    }
}
