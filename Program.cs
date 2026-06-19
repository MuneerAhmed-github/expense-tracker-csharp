using ExpenseTracker;

class Program
{
    static void Main(string[] args)
{
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        ExpenseManager manager = new ExpenseManager();
        manager.LoadFromFile();
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n===== EXPENSE TRACKER =====");
            Console.WriteLine("1.Add Expense");
            Console.WriteLine("2. View All Expenses");
            Console.WriteLine("3.Filter By Catergory");
            Console.WriteLine("4. Show Total Spending");
            Console.WriteLine("5. Exit");
            Console.WriteLine("Choose An Option");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    manager.AddExpense();
                    break;
                    case "2":
                    manager.ViewAllExpenses();
                    break;
                    case "3":
                    manager.FilterByCategory();
                    break;
                case "4":
                    manager.ShowTotalSpending();
                    break;
                    case "5":
                    running = false;
                    Console.WriteLine("GOOD BYE !");
                    break;
                default:
                    Console.WriteLine("Invalid Option Try Again !");
                    break;
            }

        }
}
}