using System;

class Person
{
    private int _age;

    // Property for Age
    public int Age
    {
        get { return _age; }
        set
        {
            if (value >= 0)
                _age = value;
            else
                Console.WriteLine("Age cannot be negative.");
        }
    }
}

class Employee
{
    private decimal _salary;

    // Property for Salary
    public decimal Salary
    {
        get { return _salary; }
        set
        {
            if (value < 18000)
            {
                throw new ArgumentException("Salary cannot be less than the minimum wage.");
            }
            _salary = value;
        }
    }
}

class Program
{
    static void Main()
    {
        // Object of Person class
        Person person = new Person();
        person.Age = 25; // Setting the age using property
        Console.WriteLine("Person's Age: " + person.Age);

        // Object of Employee class
        Employee employee = new Employee();

        try
        {
            employee.Salary = 25000; // Setting salary using property
            Console.WriteLine("Employee's Salary: " + employee.Salary);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}