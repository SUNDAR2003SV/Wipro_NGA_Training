using System;
using System.Collections.Generic;

class TaskScheduler
{
    private Queue<string> taskQueue = new Queue<string>();
    private Stack<string> undoStack = new Stack<string>();
    private List<string> allTasks = new List<string>();
    private SortedDictionary<int, string> priorityTasks = new SortedDictionary<int, string>();
    private HashSet<string> uniqueTasks = new HashSet<string>();

    // Add task
    public void AddTask(string task, int priority)
    {
        if (uniqueTasks.Add(task))
        {
            allTasks.Add(task);
            taskQueue.Enqueue(task);
            priorityTasks[priority] = task;
            Console.WriteLine($"Task '{task}' added with priority {priority}");
        }
        else
        {
            Console.WriteLine($"Duplicate task '{task}' not added.");
        }
    }

    // Execute next task (FIFO)
    public void ExecuteTask()
    {
        if (taskQueue.Count > 0)
        {
            string task = taskQueue.Dequeue();
            Console.WriteLine($"Executing task: {task}");
            undoStack.Push(task);
        }
        else
        {
            Console.WriteLine("No tasks to execute.");
        }
    }

    // Undo last executed task (LIFO)
    public void UndoLastTask()
    {
        if (undoStack.Count > 0)
        {
            string task = undoStack.Pop();
            Console.WriteLine($"Undo last executed task: {task}");
        }
        else
        {
            Console.WriteLine("No tasks to undo.");
        }
    }

    // Show all tasks
    public void ShowAllTasks()
    {
        Console.WriteLine("\nAll Tasks:");
        foreach (var task in allTasks)
        {
            Console.WriteLine(task);
        }
    }

    // Show tasks by priority
    public void ShowPriorityTasks()
    {
        Console.WriteLine("\nTasks by Priority:");
        foreach (var kvp in priorityTasks)
        {
            Console.WriteLine($"Priority {kvp.Key}: {kvp.Value}");
        }
    }
}

class Program
{
    static void Main()
    {
        TaskScheduler scheduler = new TaskScheduler();

        // Add tasks
        scheduler.AddTask("Backup Database", 1);
        scheduler.AddTask("Update Security Patches", 2);
        scheduler.AddTask("Clean Temp Files", 3);
        scheduler.AddTask("Backup Database", 1); // Duplicate

        // Execute tasks
        scheduler.ExecuteTask();
        scheduler.ExecuteTask();

        // Undo last task
        scheduler.UndoLastTask();

        // Show all tasks
        scheduler.ShowAllTasks();

        // Show tasks by priority
        scheduler.ShowPriorityTasks();
    }
}
