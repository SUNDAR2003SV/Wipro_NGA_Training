using System;
using System.Diagnostics;

class Program
{
    // Linear Search Method
    public static int LinearSearch(int[] arr, int element)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == element)
                return i;
        }
        return -1;
    }

    // Binary Search Method (array must be sorted)
    public static int BinarySearch(int[] arr, int element)
    {
        int low = 0;
        int high = arr.Length - 1;

        while (low <= high)
        {
            int mid = (low + high) / 2;

            if (arr[mid] == element)
                return mid;
            else if (arr[mid] < element)
                low = mid + 1;
            else
                high = mid - 1;
        }

        return -1;
    }

    static void Main()
    {
        Console.WriteLine("Enter the size of the array:");
        int size = int.Parse(Console.ReadLine());

        int[] arr = new int[size];

        Console.WriteLine("Enter the elements of the array:");
        for (int i = 0; i < size; i++)
        {
            arr[i] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("Enter the element to be searched:");
        int element = int.Parse(Console.ReadLine());

        // -------- Linear Search Timing --------
        Stopwatch sw1 = Stopwatch.StartNew();
        int linearResult = LinearSearch(arr, element);
        sw1.Stop();

        Console.WriteLine("\nLinear Search Result:");
        if (linearResult != -1)
            Console.WriteLine($"Found at index: {linearResult}");
        else
            Console.WriteLine("Not found");

        Console.WriteLine($"Time taken: {sw1.Elapsed.TotalMilliseconds} ms");

        // -------- Binary Search Timing --------
        Array.Sort(arr); // important for binary search

        Stopwatch sw2 = Stopwatch.StartNew();
        int binaryResult = BinarySearch(arr, element);
        sw2.Stop();

        Console.WriteLine("\nBinary Search Result:");
        if (binaryResult != -1)
            Console.WriteLine($"Found at index: {binaryResult}");
        else
            Console.WriteLine("Not found");

        Console.WriteLine($"Time taken: {sw2.Elapsed.TotalMilliseconds} ms");
    }
}