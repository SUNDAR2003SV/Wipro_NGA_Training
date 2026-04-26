using System;
using System.Text;
using System.Security.Cryptography;

class SecurePasswordSystem
{
    // Store hashed password
    static string storedHashedPassword = "";

    static void Main(string[] args)
    {
        Console.WriteLine("===== Secure Password Management and Authentication System =====");

        // US1 + US2: Create and store password securely
        Console.Write("Create a password: ");
        string password = Console.ReadLine();

        storedHashedPassword = HashPassword(password);

        Console.WriteLine("\nPassword stored securely in hashed format.");
        Console.WriteLine("Stored Hash: " + storedHashedPassword);

        // US3: Validate user login
        Console.WriteLine("\n===== Login Validation =====");
        Console.Write("Enter password to login: ");
        string loginPassword = Console.ReadLine();

        string loginHash = HashPassword(loginPassword);

        if (loginHash == storedHashedPassword)
        {
            Console.WriteLine("Login Successful! Access Granted.");
        }
        else
        {
            Console.WriteLine("Login Failed! Incorrect Password.");
        }

        // US4: Secure communication explanation
        Console.WriteLine("\n===== Secure Communication (HTTPS & SSL) =====");
        Console.WriteLine("HTTPS protects user data during transmission.");
        Console.WriteLine("SSL Certificates encrypt communication between client and server.");
        Console.WriteLine("This prevents data theft and improves security.");

        Console.WriteLine("\nPress Enter to Exit...");
        Console.ReadLine();
    }

    // US1: Hash password using SHA256
    static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();

            foreach (byte b in hashBytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}