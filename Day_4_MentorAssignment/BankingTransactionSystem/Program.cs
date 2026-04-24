using System;
using System.Collections.Generic;

class Transaction
{
    public string TransactionId { get; set; }
    public double Amount { get; set; }

    public override string ToString()
    {
        return $"TransactionId: {TransactionId}, Amount: {Amount}";
    }
}

class BankingSystem
{
    private List<Transaction> transactionHistory = new List<Transaction>();
    private Dictionary<string, double> accountBalances = new Dictionary<string, double>();
    private Queue<Transaction> pendingTransactions = new Queue<Transaction>();
    private Stack<Transaction> rollbackStack = new Stack<Transaction>();
    private HashSet<string> transactionIds = new HashSet<string>();

    // Add account
    public void AddAccount(string accountId, double initialBalance)
    {
        accountBalances[accountId] = initialBalance;
        Console.WriteLine($"Account {accountId} created with balance {initialBalance}");
    }

    // Add transaction
    public void AddTransaction(Transaction transaction)
    {
        if (transactionIds.Add(transaction.TransactionId))
        {
            pendingTransactions.Enqueue(transaction);
            Console.WriteLine($"Transaction {transaction.TransactionId} added to queue.");
        }
        else
        {
            Console.WriteLine($"Duplicate Transaction ID {transaction.TransactionId}, rejected.");
        }
    }

    // Process transactions
    public void ProcessTransactions(string accountId)
    {
        Console.WriteLine("\nProcessing Transactions:");
        while (pendingTransactions.Count > 0)
        {
            var transaction = pendingTransactions.Dequeue();
            if (accountBalances.ContainsKey(accountId))
            {
                accountBalances[accountId] += transaction.Amount;
                transactionHistory.Add(transaction);
                rollbackStack.Push(transaction);
                Console.WriteLine($"Processed {transaction} | New Balance: {accountBalances[accountId]}");
            }
            else
            {
                Console.WriteLine($"Account {accountId} not found.");
            }
        }
    }

    // Rollback last transaction
    public void RollbackLastTransaction(string accountId)
    {
        if (rollbackStack.Count > 0)
        {
            var transaction = rollbackStack.Pop();
            if (accountBalances.ContainsKey(accountId))
            {
                accountBalances[accountId] -= transaction.Amount;
                transactionHistory.Remove(transaction);
                Console.WriteLine($"Rolled back {transaction} | New Balance: {accountBalances[accountId]}");
            }
        }
        else
        {
            Console.WriteLine("No transactions to rollback.");
        }
    }

    // Show transaction history
    public void ShowTransactionHistory()
    {
        Console.WriteLine("\nTransaction History:");
        foreach (var t in transactionHistory)
        {
            Console.WriteLine(t);
        }
    }

    // Show account balances
    public void ShowBalances()
    {
        Console.WriteLine("\nAccount Balances:");
        foreach (var kvp in accountBalances)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}

class Program
{
    static void Main()
    {
        BankingSystem bank = new BankingSystem();

        // Create accounts
        bank.AddAccount("ACC1001", 5000);
        bank.AddAccount("ACC2002", 3000);

        // Add transactions
        bank.AddTransaction(new Transaction { TransactionId = "TXN001", Amount = 1000 });
        bank.AddTransaction(new Transaction { TransactionId = "TXN002", Amount = -500 });
        bank.AddTransaction(new Transaction { TransactionId = "TXN001", Amount = 200 }); // Duplicate

        // Process transactions for ACC1001
        bank.ProcessTransactions("ACC1001");

        // Show balances
        bank.ShowBalances();

        // Rollback last transaction
        bank.RollbackLastTransaction("ACC1001");

        // Show balances again
        bank.ShowBalances();

        // Show transaction history
        bank.ShowTransactionHistory();
    }
}
