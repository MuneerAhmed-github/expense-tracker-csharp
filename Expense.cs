using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    class Expense
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal Amount { get; set; }
        public string? Category { get; set; }

        public DateTime Date { get; set; }

        public void Display()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("ID             : " + Id);
            Console.WriteLine("Title          : " + Title);
            Console.WriteLine("Amount           : ₹" + Amount);
            Console.WriteLine("Category          : " + Category);
            Console.WriteLine("Date         :" + Date.ToString("dd-MM-yyyy"));
            Console.WriteLine("---------------------------------");
        }

        public string ToFileString()
        {
            return Id + "|" + Title + "|" + Amount + "|" + Category + "|" + Date.ToString("dd-MM-yyyy");
        }
    }

}
