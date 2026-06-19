using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class ExpenseManager
    {
        private List<Expense> expenses = new List<Expense>();
        private string filePath = "expenses.txt";
        private int nextId = 1;

        public void LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No Previous Data Found Starting Fresh");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                Expense e = new Expense();
                e.Id = int.Parse(parts[0]);
                e.Title = parts[1];
                e.Amount = decimal.Parse(parts[2]);
                e.Category = parts[3];
                e.Date = DateTime.ParseExact(parts[4], "dd-MM-yyyy", null);
                expenses.Add(e);
                nextId = e.Id + 1;

            }
            Console.WriteLine("Loaded " + expenses.Count + "expenses from file.");
        }

        public void SaveToFile()
        {
            List<string> lines = new List<string>();
            foreach (Expense e in expenses)
            {
                lines.Add(e.ToFileString());
            }
            File.WriteAllLines(filePath, lines);
        }

        public void AddExpense()
        {
            Console.WriteLine("\n--- Add New Expense ---");
            Console.Write("Enter Title: ");
            string? title = Console.ReadLine();

            Console.Write("Enter Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine()!);
            Console.WriteLine("Categories: Food | Travel | Shopping | Bills | Entertainment | Other");
            Console.Write("Enter Category: ");
            string? category = Console.ReadLine();
            Expense e = new Expense();
            e.Id = nextId++;
            e.Title = title;
            e.Category = category;
            e.Amount = amount;
            e.Date = DateTime.Now;
            expenses.Add(e);
            SaveToFile();
            Console.WriteLine("Expenses Added SuceesFully !");
        }

        public void ViewAllExpenses()
        {
            Console.WriteLine("\n---- All Expenses ---");
            if (expenses.Count == 0)
            {
                Console.WriteLine("No Expenses Found !");
                return;
            }
            foreach (Expense e in expenses)
            {
                e.Display();
            }
        }

        public void FilterByCategory()
        {
            Console.Write("Enter Category to Filter: ");
            string? category = Console.ReadLine();
            var filtered = expenses.Where(e => e.Category == category).ToList();
            if (filtered.Count == 0)
            {
                Console.WriteLine("No Expenses Found in this Category !");
                return;
            }
            foreach (Expense e in filtered)
            {
                e.Display();
            }
        }
        public void ShowTotalSpending()
        {
            Console.WriteLine("\n--- Total Spending ---");

            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses to calculate!");
                return;
            }

            decimal total = expenses.Sum(e => e.Amount);
            Console.WriteLine("Total Spent: ₹" + total);
        }
    }

}
