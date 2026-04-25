using System;

// Delegate
public delegate void OrderPlacedHandler(Order order);

// Order class
public class Order
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public double Amount { get; set; }
}

// OrderProcessor class (Publisher)
public class OrderProcessor
{
    // Event using delegate
    public event OrderPlacedHandler OnOrderPlaced;

    public void PlaceOrder(Order order)
    {
        Console.WriteLine("Order Placed: " + order.OrderId);

        // Null-safe event invocation
        OnOrderPlaced?.Invoke(order);
    }
}

// Subscriber Class - Email Service
public class EmailService
{
    public void SendEmail(Order order)
    {
        Console.WriteLine("Email sent to customer");
    }
}

// Subscriber Class - SMS Service
public class SMSService
{
    public void SendSMS(Order order)
    {
        Console.WriteLine("SMS sent to customer");
    }
}

// Subscriber Class - Logger Service
public class LoggerService
{
    public void LogOrder(Order order)
    {
        Console.WriteLine("Order logged successfully");
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
        // Create objects
        OrderProcessor processor = new OrderProcessor();
        EmailService emailService = new EmailService();
        SMSService smsService = new SMSService();
        LoggerService loggerService = new LoggerService();

        // Subscribe methods to event (Multicast Delegate)
        processor.OnOrderPlaced += emailService.SendEmail;
        processor.OnOrderPlaced += smsService.SendSMS;
        processor.OnOrderPlaced += loggerService.LogOrder;

        // Create Order
        Order order = new Order
        {
            OrderId = 101,
            CustomerName = "Nandhini",
            Amount = 2500.50
        };

        // Process Order
        processor.PlaceOrder(order);

        // Optional: Unsubscribe SMS service
        // processor.OnOrderPlaced -= smsService.SendSMS;

        Console.ReadLine();
    }
}