using System.Diagnostics;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("===== SYNCHRONOUS EXECUTION =====");
        RunSynchronous();

        Console.WriteLine("\n===== ASYNCHRONOUS EXECUTION =====");
        await RunAsynchronous();

        Console.ReadLine();
    }

    // ---------------- SYNCHRONOUS ----------------
    static void RunSynchronous()
    {
        Stopwatch sw = Stopwatch.StartNew();

        GetStudentDetails();
        GetMarks();
        GetAttendance();

        sw.Stop();
        Console.WriteLine($"Total Time (Sync): {sw.ElapsedMilliseconds} ms");
    }

    static void GetStudentDetails()
    {
        Thread.Sleep(2000); // Simulate delay
        Console.WriteLine("Student Details Loaded");
    }

    static void GetMarks()
    {
        Thread.Sleep(2000);
        Console.WriteLine("Marks Loaded");
    }

    static void GetAttendance()
    {
        Thread.Sleep(2000);
        Console.WriteLine("Attendance Loaded");
    }
    // ---------------- ASYNCHRONOUS ----------------
    static async Task RunAsynchronous()
    {
        Stopwatch sw = Stopwatch.StartNew();

        Task task1 = GetStudentDetailsAsync();
        Task task2 = GetMarksAsync();
        Task task3 = GetAttendanceAsync();

        await Task.WhenAll(task1, task2, task3);

        sw.Stop();
        Console.WriteLine($"Total Time (Async): {sw.ElapsedMilliseconds} ms");
    }

    static async Task GetStudentDetailsAsync()
    {
        await Task.Delay(2000);
        Console.WriteLine("Student Details Loaded");
    }

    static async Task GetMarksAsync()
    {
        await Task.Delay(2000);
        Console.WriteLine("Marks Loaded");
    }

    static async Task GetAttendanceAsync()
    {
        await Task.Delay(2000);
        Console.WriteLine("Attendance Loaded");
    }
}