class Expense
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public decimal Amount  { get; set; }
    public string? Category { get; set; }

    public DateTime Date{ get; set; }

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

        string[] lines=File.ReadAllLines(filePath);
        foreach(string line in lines)
        {
            string[] parts=line.Split('|');
            Expense e=new Expense();
            e.Id=int.Parse(parts[0]);
            e.Title=parts[1];
            e.Amount=decimal.Parse(parts[2]);
            e.Category=parts[3];
            e.Date = DateTime.ParseExact(parts[4], "dd-MM-yyyy", null);
            expenses.Add(e);
            nextId = e.Id + 1;

        }
        Console.WriteLine("Loaded "+expenses.Count+"expenses from file.");
    } 
    
    public void SaveToFile()
    {
        List<string> lines = new List<string>();
        foreach(Expense e in expenses)
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
        string? category= Console.ReadLine();
        var filtered=expenses.Where(e=>e.Category == category).ToList();
        if (filtered.Count == 0)
        {
            Console.WriteLine("No Expenses Found in this Category !");
            return;
        }
        foreach(Expense e in filtered)
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