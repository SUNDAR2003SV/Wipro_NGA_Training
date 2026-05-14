using System;
using System.Linq;

class Program
{
    static void Main()
    {
        using var context = new AppDbContext();

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n===== STUDENT CRUD MENU =====");
            Console.WriteLine("1. Create Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateStudent(context);
                    break;

                case 2:
                    ReadStudents(context);
                    break;

                case 3:
                    UpdateStudent(context);
                    break;

                case 4:
                    DeleteStudent(context);
                    break;

                case 5:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    // ================= CREATE =================
    static void CreateStudent(AppDbContext context)
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Age: ");
        int age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Department: ");
        string dept = Console.ReadLine();

        var student = new Student
        {
            Name = name,
            Age = age,
            Department = dept
        };

        context.Students.Add(student);
        context.SaveChanges();

        Console.WriteLine("Student Added Successfully!");
    }

    // ================= READ =================
    static void ReadStudents(AppDbContext context)
    {
        var students = context.Students.ToList();

        Console.WriteLine("\n----- STUDENT LIST -----");

        foreach (var s in students)
        {
            Console.WriteLine($"ID: {s.Id}, Name: {s.Name}, Age: {s.Age}, Dept: {s.Department}");
        }
    }

    // ================= UPDATE =================
    static void UpdateStudent(AppDbContext context)
    {
        Console.Write("Enter Student ID to Update: ");
        int id = Convert.ToInt32(Console.ReadLine());

        var student = context.Students.FirstOrDefault(s => s.Id == id);

        if (student != null)
        {
            Console.Write("Enter New Name: ");
            student.Name = Console.ReadLine();

            Console.Write("Enter New Age: ");
            student.Age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter New Department: ");
            student.Department = Console.ReadLine();

            context.SaveChanges();

            Console.WriteLine("Student Updated Successfully!");
        }
        else
        {
            Console.WriteLine("Student Not Found!");
        }
    }

    // ================= DELETE =================
    static void DeleteStudent(AppDbContext context)
    {
        Console.Write("Enter Student ID to Delete: ");
        int id = Convert.ToInt32(Console.ReadLine());

        var student = context.Students.FirstOrDefault(s => s.Id == id);

        if (student != null)
        {
            context.Students.Remove(student);
            context.SaveChanges();

            Console.WriteLine("Student Deleted Successfully!");
        }
        else
        {
            Console.WriteLine("Student Not Found!");
        }
    }
}