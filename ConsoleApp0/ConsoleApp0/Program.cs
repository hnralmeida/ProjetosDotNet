
namespace ConsoleApp0;

public class Program
{
    public static void Main(string[] args)
    {
        int choice=5;
        BankAccount account = new BankAccount(1000, "John Doe");

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
    public BankAccount(double initialBalance, string accountName)
    {
        balance = initialBalance;
        name = accountName;
    }
    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            balance += amount;
            Console.WriteLine($"Deposited: {amount}. New balance: {balance}");
        }
        else
        {
            Console.WriteLine("Deposit amount must be positive.");
        }
    }
    public void Withdraw(double amount)
    {
        if (amount > 0 && amount <= balance)
        {
            balance -= amount;
            Console.WriteLine($"Withdrew: {amount}. New balance: {balance}");
        }
        else
        {
            Console.WriteLine("Invalid withdrawal amount.");
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