
namespace ConsoleApp0;

public class Program
{
    public static void Main(string[] args)
    {
        int choice=5;
        DataStore<BankAccount> accountStore = new DataStore<BankAccount>();
        ILogger logger = new FileLogger("log.txt");
        accountStore.Add(new BankAccount(1000, "John Doe", logger));
        accountStore.Add(new BankAccount(500, "Charlie Brown", logger));
        accountStore.Add(new BankAccount(1200, "Mary Lane", logger));

        Console.WriteLine("Pick your account:");
        for(int i = 0; i < accountStore.GetAll().Count(); i++)
        {
            var account0 = accountStore.GetAll().ElementAt(i);
            Console.WriteLine($"{i + 1}. {account0.GetName()}");
        }
        int pick = int.Parse(Console.ReadLine());
        var account = accountStore.GetAll().ElementAt(pick - 1);

        while (choice!=0) {
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Check Balance");
            Console.WriteLine("4. Check Name");
            Console.WriteLine("0. Exit");
            choice = int.Parse(Console.ReadLine());

            switch (choice) {
                case 1:
                    Console.WriteLine("Enter amount to deposit:");
                    double depositAmount = double.Parse(Console.ReadLine());
                    account.Deposit(depositAmount);
                    break;
                case 2:
                    Console.WriteLine("Enter amount to withdraw:");
                    double withdrawAmount = double.Parse(Console.ReadLine());
                    account.Withdraw(withdrawAmount);
                    break;
                case 3:
                    Console.WriteLine($"Current balance: {account.GetBalance()}");
                    break;
                case 4:
                    Console.WriteLine($"Account name: {account.GetName()}");
                    break;
                case 0:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}

class BankAccount
{
    private string name;
    private double balance;
    private readonly ILogger logger;
    public BankAccount(double initialBalance, string accountName, ILogger logger)
    {
        balance = initialBalance;
        name = accountName;
        this.logger = logger;
    }
    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            balance += amount;
            Console.WriteLine($"Deposited: {amount}. New balance: {balance}");
            logger.Log($"User deposited: {amount}. New balance: {balance}");
        }
        else
        {
            Console.WriteLine("Deposit amount must be positive.");
            logger.Log($"The user attempted to deposit an invalid amount: {amount}");
        }
    }
    public void Withdraw(double amount)
    {
        if (amount > 0 && amount <= balance)
        {
            balance -= amount;
            Console.WriteLine($"Withdrew: {amount}. New balance: {balance}");
            logger.Log($"User withdrew: {amount}. New balance: {balance}");
        }
        else
        {
            Console.WriteLine("Invalid withdrawal amount.");
            logger.Log($"The user attempted to withdraw an invalid amount: {amount}");
        }
    }
    public double GetBalance()
    {
        return balance;
    }

    public string GetName()
    {
        return name;
    }

}

class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}

interface ILogger
{
    void Log(string message);
}

class FileLogger : ILogger
{
    private readonly string filePath;
    public FileLogger(string filePath)
    {
        this.filePath = filePath;
    }
    public void Log(string message)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"Log: {message}");
        }
    }
}

class DataStore<T>
{
    private readonly List<T> items = new List<T>();
    public void Add(T item)
    {
        items.Add(item);
    }
    public IEnumerable<T> GetAll()
    {
        return items;
    }
}

// delegate void LogHandler(string message);