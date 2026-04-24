using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentActivityEvaluation
{
    // Delegate to handle student evaluation logic
    public delegate void StudentEvaluation(Student student);

    public class Student
    {
        public string Name { get; set; }
        public int Marks { get; set; }
        public int Attendance { get; set; }
        public int Participation { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Sample student list
            List<Student> students = new List<Student>
            {
                new Student { Name = "John Snow", Marks = 75, Attendance = 90, Participation = 80 },
                new Student { Name = "Tyrion", Marks = 55, Attendance = 85, Participation = 75 },
                new Student { Name = "Joffrey", Marks = 45, Attendance = 70, Participation = 60 }
            };

            // Anonymous method to calculate total marks and display performance
            StudentEvaluation evaluate = delegate (Student s)
            {
                int totalScore = s.Marks + s.Attendance + s.Participation;
                Console.WriteLine($"Student: {s.Name}, Total Score: {totalScore}");
            };

            // Lambda expression to check eligibility (marks > 50)
            Func<Student, bool> isEligible = s => s.Marks > 50;

            // Evaluate each student
            foreach (var student in students)
            {
                evaluate(student);
            }

            // Filter students based on eligibility using lambda
            var eligibleStudents = students.Where(isEligible).ToList();

            Console.WriteLine("\nEligible Students:");
            foreach (var student in eligibleStudents)
            {
                Console.WriteLine(student.Name);
            }
        }
    }
}
