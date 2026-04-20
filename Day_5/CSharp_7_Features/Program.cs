using System;

class Program
{
    static void Main()
    {
        // 1. Tuples
        var person = GetPerson();
        Console.WriteLine($"Tuple: Name = {person.Name}, Age = {person.Age}");

        // 2. Pattern Matching
        object obj = person.Age;
        if (obj is int age)
        {
            Console.WriteLine($"Pattern Matching: Age is {age}");
        }

        // 3. Out Variables
        Console.Write("Enter a number: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int number))
        {
            Console.WriteLine($"Out Variable: You entered {number}");

            // 4. Local Function
            int Square(int n)
            {
                return n * n;
            }

            Console.WriteLine($"Local Function: Square = {Square(number)}");
        }
        else
        {
            Console.WriteLine("Invalid input!");
        }
    }

    // Tuple Method
    static (string Name, int Age) GetPerson()
    {
        return ("John Snow", 25);
    }
}