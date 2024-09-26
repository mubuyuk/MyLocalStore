using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalStore
{
     class Customer //Kund class som ska lagra namn, lösenord & kundkorg.
     {
        public string Name { get; private set; }
        public string Password { get; private set; }
        public List<Product> Cart { get; private set; }


        // Constructor med  parametrar, har samma naman som classen, har ingen returtyp
        public Customer(string name, string password) 
        {
            Name = name;                // Tilldela parametrarna till fällten (Fields)
            Password = password;
            Cart = new List<Product>();
        }

    }

}
