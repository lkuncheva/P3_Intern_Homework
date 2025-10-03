using System.Text;

namespace ExceptionsHomework;

public class ExceptionsHomework
{
    public static T[] Subsequence<T>(T[] arr, int startIndex, int count)
    {
        if (arr == null)
        {
            throw new ArgumentNullException("Array cannot be null", nameof(arr));
        }

        if (startIndex < 0 || startIndex > arr.Length)
        {
            throw new ArgumentOutOfRangeException("Start index out of bounds", nameof(startIndex));
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException("Count cannot be negative.", nameof(count));
        }

        if ((startIndex + count) > arr.Length)
        {
            throw new ArgumentException("Start index + count cannot be larger than the array length", nameof(count));
        }

        List<T> result = new List<T>();

        for (int i = startIndex; i < startIndex + count; i++)
        {
            result.Add(arr[i]);
        }

        return result.ToArray();
    }

    public static string ExtractEnding(string str, int count)
    {
        if (string.IsNullOrEmpty(str))
        {
            throw new ArgumentNullException("String cannot be null, empty.", nameof(str));
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException("Count cannot be negative.", nameof(count));
        }

        if (count > str.Length)
        {
            throw new ArgumentOutOfRangeException("Count cannot be larger than the array length", nameof(count));
        }

        StringBuilder result = new StringBuilder();

        for (int i = str.Length - count; i < str.Length; i++)
        {
            result.Append(str[i]);
        }

        return result.ToString();
    }

    public static bool IsPrime(int number)
    {
        if (number <= 1)
        {
            throw new ArgumentOutOfRangeException("Primality check is valid only for numbers greater than 1.", nameof(number));
        }

        if (number == 2 || number == 3)
        {
            return true;
        }

        for (int divisor = 2; divisor <= Math.Sqrt(number); divisor++)
        {
            if (number % divisor == 0)
            {
                return false;
            }
        }

        return true;
    }

    public static void Main()
    {
        Console.WriteLine("--- Subsequence Tests ---");
        try
        {
            var substr = Subsequence("Hello!".ToCharArray(), 2, 3);
            Console.WriteLine($"Subsequence: {new string(substr)}");

            var subarr = Subsequence(new int[] { -1, 3, 2, 1 }, 0, 2);
            Console.WriteLine($"Subsequence: {String.Join(" ", subarr)}");

            var invalidSubsequence = Subsequence(new int[] { 1, 2 }, 1, 5);
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] Subsequence failed: {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"[ERROR] An unexpected error occurred: {ex.Message}");
            Console.ResetColor();

            throw;
        }

        Console.WriteLine("\n--- ExtractEnding Tests ---");
        try
        {
            Console.WriteLine($"'I love C#' ending 2: {ExtractEnding("I love C#", 2)}");
            Console.WriteLine($"'Nakov' ending 4: {ExtractEnding("Nakov", 4)}");
            Console.WriteLine($"'beer' ending 4: {ExtractEnding("beer", 4)}");

            Console.WriteLine($"'Hi' ending 100: {ExtractEnding("Hi", 100)}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] ExtractEnding failed: {ex.Message}");
            Console.ResetColor();
        }
        catch (ArgumentNullException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] ExtractEnding failed: {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"[ERROR] An unexpected error occurred: {ex.Message}");
            Console.ResetColor();

            throw;
        }

        Console.WriteLine("\n--- Primality Check Tests ---");
        try
        {
            if (IsPrime(23))
            {
                Console.WriteLine("23 is prime.");
            }
            else
            {
                Console.WriteLine("23 is not prime.");
            }

            if (IsPrime(33))
            {
                Console.WriteLine("33 is prime.");
            }
            else
            {
                Console.WriteLine("33 is not prime.");
            }

            IsPrime(1);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] Primality check failed: {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"[ERROR] An unexpected error occurred: {ex.Message}");
            Console.ResetColor();

            throw;
        }


        Console.WriteLine("\n--- Student and Exam Tests ---");
        try
        {
            List<Exam> peterExams = new List<Exam>()
            {
                new SimpleMathExam(2),
                new CSharpExam(55),
                new CSharpExam(100),
                new SimpleMathExam(1),
                new CSharpExam(0),
            };
            Student peter = new Student("Peter", "Petrov", peterExams);

            // Test invalid student creation
            // Student invalidStudent = new Student(null, "Test");

            // Test invalid exam creation
            // CSharpExam invalidExam = new CSharpExam(101); 

            double peterAverageResult = peter.CalcAverageExamResultInPercents();
            Console.WriteLine($"Average results for {peter.FirstName} = {peterAverageResult:p0}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] Exam setup failed: {ex.Message}");
            Console.ResetColor();
        }
        catch (ArgumentNullException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] Student/Exam setup failed: {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"[FATAL ERROR] An unexpected application error occurred: {ex.Message}");
            Console.ResetColor();

            throw;
        }
    }
}