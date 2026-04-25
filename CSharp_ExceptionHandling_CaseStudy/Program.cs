using System;

// Custom exception for insufficient balance
public class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException(string message) : base(message) { }
}

// Custom exception for invalid deposit amount
public class InvalidAmountException : Exception
{
    public InvalidAmountException(string message) : base(message) { }
}

public class BankAccount
{
    public string AccountHolderName { get; set; }
    public double Balance { get; private set; }

    public BankAccount(string name, double initialBalance)
    {
        AccountHolderName = name;
        Balance = initialBalance;
    }

    public void Deposit(double amount)
    {
        if (amount <= 0)
        {
            throw new InvalidAmountException("Deposit amount must be greater than 0.");
        }
        Balance += amount;
        Console.WriteLine($"Successfully deposited ₹{amount}. Current Balance: ₹{Balance}");
    }

    public void Withdraw(double amount)
    {
        if (amount > Balance)
        {
            throw new InsufficientBalanceException("Withdrawal amount exceeds available balance.");
        }
        if (Balance - amount < 1000)
        {
            throw new InsufficientBalanceException("Withdrawal denied. Minimum balance of ₹1000 must be maintained.");
        }
        Balance -= amount;
        Console.WriteLine($"Successfully withdrew ₹{amount}. Current Balance: ₹{Balance}");
    }

    public void CheckBalance()
    {
        Console.WriteLine($"Account Holder: {AccountHolderName}, Current Balance: ₹{Balance}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        BankAccount account = new BankAccount("Ravi Kumar", 5000);

        try
        {
            account.CheckBalance();

            // Valid deposit
            account.Deposit(2000);

            // Invalid deposit
            account.Deposit(-500);

            // Valid withdrawal
            account.Withdraw(3000);

            // Withdrawal exceeding balance
            account.Withdraw(6000);

            // Withdrawal causing balance < 1000
            account.Withdraw(5500);
        }
        catch (InvalidAmountException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (InsufficientBalanceException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Argument Error: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Operation Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Transaction attempt completed.");
        }

        account.CheckBalance();
    }
}
