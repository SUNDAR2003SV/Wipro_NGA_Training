using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Step 1: Define file paths
        string filepath = "sample.txt";
   

        try
        {
            // Step 2: Create file
            Console.WriteLine("Creating a file...");
            File.Create(filepath).Close(); // Closing the file is important

            // Step 3: Write data to file
            Console.WriteLine("Writing to the file...");
            File.WriteAllText(filepath, "Hello, this is the first line of the file..!!");

            // Step 4: Append data to file
            Console.WriteLine("Appending data to the file...");
            File.AppendAllText(filepath, "\nThis data is appended to the text.");

            // Step 5: Read from file
            Console.WriteLine("Reading from the file...");
            string content = File.ReadAllText(filepath);
            Console.WriteLine(content);
          
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}