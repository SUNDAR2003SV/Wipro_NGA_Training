using System;

namespace reflectionExample


{
    internal class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}");
        }
        public void Exam()
        {
            Console.WriteLine("Student is taking an exam.");
        }
    }
}
