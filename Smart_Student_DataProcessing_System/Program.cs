using System;
using System.IO;

// Custom Attribute
[AttributeUsage(AttributeTargets.Class)]
public class StudentInfoAttribute : Attribute
{
    public string Info { get; set; }

    public StudentInfoAttribute(string info)
    {
        Info = info;
    }
}

// Student Class
[StudentInfo("Student Data Class")]
public class Student
{
    // Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public int Marks { get; set; }

    public Student(int id, string name, int marks)
    {
        Id = id;
        Name = name;
        Marks = marks;
    }

    public void Display()
    {
        Console.WriteLine(Id + " " + Name + " " + Marks);
    }
}

// Student Manager with Indexer
public class StudentManager
{
    private Student[] students = new Student[5];

    // Indexer
    public Student this[int index]
    {
        get { return students[index]; }
        set { students[index] = value; }
    }

    // Linear Search
    public int LinearSearch(int id)
    {
        for (int i = 0; i < students.Length; i++)
        {
            if (students[i] != null && students[i].Id == id)
                return i;
        }
        return -1;
    }

    // Bubble Sort by Marks
    public void BubbleSort()
    {
        for (int i = 0; i < students.Length - 1; i++)
        {
            for (int j = 0; j < students.Length - i - 1; j++)
            {
                if (students[j].Marks > students[j + 1].Marks)
                {
                    Student temp = students[j];
                    students[j] = students[j + 1];
                    students[j + 1] = temp;
                }
            }
        }
    }

    // Binary Search (after sorting by Id)
    public int BinarySearch(int id)
    {
        int left = 0, right = students.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (students[mid].Id == id)
                return mid;
            else if (students[mid].Id < id)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1;
    }

    public void DisplayAll()
    {
        foreach (Student s in students)
        {
            if (s != null)
                s.Display();
        }
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
        StudentManager manager = new StudentManager();

        manager[0] = new Student(101, "John Snow", 86);
        manager[1] = new Student(102, "Joffrey", 73);
        manager[2] = new Student(103, "Tyrion", 89);
        manager[3] = new Student(104, "Cersei", 66);
        manager[4] = new Student(105, "Arya", 90);

        Console.WriteLine("Student Records:");
        manager.DisplayAll();

        // Linear Search
        Console.WriteLine("\nLinear Search for ID 103:");
        int result = manager.LinearSearch(103);
        Console.WriteLine(result != -1 ? "Found at index: " + result : "Not Found");

        // Bubble Sort
        Console.WriteLine("\nSorted by Marks (Bubble Sort):");
        manager.BubbleSort();
        manager.DisplayAll();

        // File Handling
        string filePath = "students.txt";
        string copyPath = "students_backup.txt";

        // Create + Write
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine("101 Jamie 80");
            sw.WriteLine("102 Ned Stark 85");
        }

        // Append
        using (StreamWriter sw = File.AppendText(filePath))
        {
            sw.WriteLine("103 Daenerys 95");
        }

        // Read
        Console.WriteLine("\nReading File:");
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }

        // Copy File
        File.Copy(filePath, copyPath, true);
        Console.WriteLine("\nFile copied successfully");

        // Delete File
        File.Delete(copyPath);
        Console.WriteLine("Backup file deleted successfully");

        // Algorithm Comparison
        Console.WriteLine("\nAlgorithm Comparison:");
        Console.WriteLine("Linear Search - Time: O(n), Space: O(1)");
        Console.WriteLine("Binary Search - Time: O(log n), Space: O(1)");
        Console.WriteLine("Bubble Sort - Time: O(n^2), Space: O(1)");
        Console.WriteLine("Best for small dataset: Bubble Sort");
        Console.WriteLine("Best for large dataset: Binary Search");
    }
}