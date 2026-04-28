using System;
using System.Collections.Generic;

// --- Interfaces ---
public interface IPaymentMethod
{
    void Pay(decimal amount);
}

public interface INotificationService
{
    void Notify(string message);
}

public interface ITransactionHistory
{
    void AddTransaction(string transaction);
    IEnumerable<string> GetTransactions();
}

// --- Payment Implementations ---
public class UpiPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} via UPI.");
    }
}

public class CardPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} via Card.");
    }
}

public class NetBankingPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} via Net Banking.");
    }
}

// --- Notification Implementation ---
public class EmailNotification : INotificationService
{
    public void Notify(string message)
    {
        Console.WriteLine($"Email Notification: {message}");
    }
}

public class SmsNotification : INotificationService
{
    public void Notify(string message)
    {
        Console.WriteLine($"SMS Notification: {message}");
    }
}

// --- Transaction History Implementation ---
public class TransactionHistory : ITransactionHistory
{
    private readonly List<string> _transactions = new List<string>();

    public void AddTransaction(string transaction)
    {
        _transactions.Add(transaction);
    }

    public IEnumerable<string> GetTransactions() => _transactions;
}

// --- Wallet Class ---
public class Wallet
{
    private decimal _balance;
    private readonly INotificationService _notificationService;
    private readonly ITransactionHistory _transactionHistory;

    public Wallet(INotificationService notificationService, ITransactionHistory transactionHistory)
    {
        _notificationService = notificationService;
        _transactionHistory = transactionHistory;
    }

    public void AddMoney(decimal amount)
    {
        _balance += amount;
        _transactionHistory.AddTransaction($"Added {amount} to wallet.");
        _notificationService.Notify($"Wallet credited with {amount}. Current balance: {_balance}");
    }

    public void MakePayment(IPaymentMethod paymentMethod, decimal amount)
    {
        if (_balance >= amount)
        {
            paymentMethod.Pay(amount);
            _balance -= amount;
            _transactionHistory.AddTransaction($"Paid {amount} using {paymentMethod.GetType().Name}.");
            _notificationService.Notify($"Payment of {amount} successful. Remaining balance: {_balance}");
        }
        else
        {
            _notificationService.Notify("Insufficient balance!");
        }
    }

    public void ShowTransactions()
    {
        Console.WriteLine("Transaction History:");
        foreach (var t in _transactionHistory.GetTransactions())
        {
            Console.WriteLine(t);
        }
    }
}

// --- Demo ---
public class Program
{
    public static void Main()
    {
        INotificationService notificationService = new EmailNotification();
        ITransactionHistory transactionHistory = new TransactionHistory();
        Wallet wallet = new Wallet(notificationService, transactionHistory);

        wallet.AddMoney(1000);

        wallet.MakePayment(new UpiPayment(), 200);
        wallet.MakePayment(new CardPayment(), 300);
        wallet.MakePayment(new NetBankingPayment(), 600); // Should fail due to insufficient balance

        wallet.ShowTransactions();
    }
}
