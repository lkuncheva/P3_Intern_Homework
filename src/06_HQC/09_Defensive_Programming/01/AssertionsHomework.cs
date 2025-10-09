using System.Diagnostics;

public class AssertionsHomework
{
    public static void SelectionSort<T>(T[] arr) where T : IComparable<T>
    {
        Debug.Assert(arr != null, "Precondition failed: Array to be sorted cannot be null.");

        int originalLength = arr.Length;

        for (int index = 0; index < arr.Length - 1; index++)
        {
            int minElementIndex = FindMinElementIndex(arr, index, arr.Length - 1);
            Swap(ref arr[index], ref arr[minElementIndex]);
        }

        Debug.Assert(arr.Length == originalLength, "Postcondition failed: Array length changed after sorting.");
        Debug.Assert(IsSorted(arr), "Postcondition failed: Array is not sorted after SelectionSort.");
    }

    private static int FindMinElementIndex<T>(T[] arr, int startIndex, int endIndex)
        where T : IComparable<T>
    {
        Debug.Assert(arr != null, "Precondition failed: Array cannot be null.");
        Debug.Assert(startIndex >= 0 && startIndex < arr.Length, "Precondition failed: startIndex is out of bounds.");
        Debug.Assert(endIndex >= 0 && endIndex < arr.Length, "Precondition failed: endIndex is out of bounds.");
        Debug.Assert(startIndex <= endIndex, "Precondition failed: startIndex must be <= endIndex.");

        int minElementIndex = startIndex;

        for (int i = startIndex + 1; i <= endIndex; i++)
        {
            if (arr[i].CompareTo(arr[minElementIndex]) < 0)
            {
                minElementIndex = i;
            }
        }

        Debug.Assert(minElementIndex >= startIndex && minElementIndex <= endIndex,
                     "Postcondition failed: Min element index is outside the search range.");


        for (int i = startIndex; i <= endIndex; i++)
        {
            Debug.Assert(arr[minElementIndex].CompareTo(arr[i]) <= 0,
                         "Postcondition failed: Element at returned index is not the actual minimum in the range.");
        }

        return minElementIndex;
    }

    private static void Swap<T>(ref T x, ref T y)
    {
        T oldX = x;
        x = y;
        y = oldX;
    }

    public static int BinarySearch<T>(T[] arr, T value) where T : IComparable<T>
    {
        Debug.Assert(arr != null, "Precondition failed: Array for BinarySearch cannot be null.");
        Debug.Assert(IsSorted(arr), "Precondition failed: Array must be sorted for BinarySearch.");

        return BinarySearch(arr, value, 0, arr.Length - 1);
    }

    private static int BinarySearch<T>(T[] arr, T value, int startIndex, int endIndex)
        where T : IComparable<T>
    {
        Debug.Assert(startIndex >= 0 && endIndex < arr.Length, "Precondition failed: Indices are out of array bounds.");
        Debug.Assert(startIndex <= arr.Length, "Precondition failed: Start index should not exceed array length.");
        Debug.Assert(endIndex >= -1, "Precondition failed: End index should not be less than -1.");

        int originalStartIndex = startIndex;
        int originalEndIndex = endIndex;

        while (startIndex <= endIndex)
        {
            int midIndex = (startIndex + endIndex) / 2;

            if (arr[midIndex].Equals(value))
            {
                Debug.Assert(arr[midIndex].Equals(value), "Postcondition failed: Found index does not contain the expected value.");
                Debug.Assert(midIndex >= originalStartIndex && midIndex <= originalEndIndex, "Postcondition failed: Found index is outside the initial search range.");
                return midIndex;
            }

            if (arr[midIndex].CompareTo(value) < 0)
            {
                startIndex = midIndex + 1;
            }
            else
            {
                endIndex = midIndex - 1;
            }
        }

        return -1;
    }

    private static bool IsSorted<T>(T[] arr) where T : IComparable<T>
    {
        if (arr == null)
        {
            return false;
        }

        if (arr.Length <= 1)
        {
            return true;
        }

        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i].CompareTo(arr[i + 1]) > 0)
            {
                return false;
            }
        }

        return true;
    }

    static void Main()
    {
        Console.WriteLine("--- Selection Sort Demonstration ---");

        int[] arr = new int[] { 3, -1, 15, 4, 17, 2, 33, 0 };

        Console.WriteLine($"Original array: [{string.Join(", ", arr)}]");

        SelectionSort(arr);
        Console.WriteLine($"Sorted array:   [{string.Join(", ", arr)}]");

        Console.WriteLine("\nTesting edge cases for sorting:");
        SelectionSort(new int[0]);
        Console.WriteLine("Sorting empty array successful.");
        SelectionSort(new int[1] { 42 });
        Console.WriteLine("Sorting single element array successful.");

        Console.WriteLine("\n--- Binary Search Demonstration ---");


        Console.WriteLine($"Index of -1000 (not found): {BinarySearch(arr, -1000)}");
        Console.WriteLine($"Index of 0 (found at index 1): {BinarySearch(arr, 0)}");
        Console.WriteLine($"Index of 17 (found at index 6): {BinarySearch(arr, 17)}");
        Console.WriteLine($"Index of 10 (not found): {BinarySearch(arr, 10)}");
        Console.WriteLine($"Index of 33 (found at index 7): {BinarySearch(arr, 33)}");
        Console.WriteLine($"Index of 1000 (not found): {BinarySearch(arr, 1000)}");

        // Example of a failed assertion:
        // int[] unsortedArr = new int[] { 5, 1 };
        // Console.WriteLine(BinarySearch(unsortedArr, 1));
    }
}