#nullable enable
using System;

class Program
{
    static void Main()
    {
        // 1. Nullable Reference Type (Null Safety)
        string? name = null;

        // Null-conditional + Null-coalescing
        Console.WriteLine($"Name Length: {name?.Length ?? 0}");

        // Assign value
        name = "John Snow";
        Console.WriteLine($"Name Length after assignment: {name?.Length}");

        // 2. Switch Expression
        Console.Write("Enter a number (1-3): ");
        int num = int.Parse(Console.ReadLine());

        string result = num switch
        {
            1 => "One",
            2 => "Two",
            3 => "Three",
            _ => "Invalid Number"
        };

        Console.WriteLine($"Switch Expression Result: {result}");
    }
}