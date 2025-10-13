using System.Diagnostics;
using System.Globalization;

namespace MathPerformanceComparison;

public class Program
{
    private const long FAST_TYPE_ITERATIONS = 500_000_000;
    private const long DECIMAL_ITERATIONS = 50_000_000;

    private const double BASE_VALUE = 4.2000000000000001;

    public static void Main(string[] args)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

        Console.WriteLine("--- Advanced Math Function Performance Comparison ---");
        Console.WriteLine($"Fast Type Iterations: {FAST_TYPE_ITERATIONS:N0}");
        Console.WriteLine($"Decimal Iterations:   {DECIMAL_ITERATIONS:N0}\n");

        Console.WriteLine(
            "{0,-10} | {1,-14} | {2,-14} | {3,-14}",
            "Type", "Square Root (ms)", "Natural Log (ms)", "Sine (ms)"
        );
        Console.WriteLine(new string('-', 69));

        TestDouble();
        TestFloat();
        TestDecimal();

        Console.WriteLine(new string('-', 69));
        Console.WriteLine("\nAll times are in milliseconds (ms). Lower is better.");
        Console.WriteLine("P.S. Decimal is always the slowest because it handles a lot more precision.");
        Console.WriteLine("P.P.S. Decimal multiplication uses a tiny number to prevent the result from overflowing.");
    }

    private static void TestDouble()
    {
        double value = BASE_VALUE;
        long iterations = FAST_TYPE_ITERATIONS;
        Console.Write("{0,-10} | ", "double");

        TimeOperation(iterations, value, (double v) => Math.Sqrt(v));
        TimeOperation(iterations, value, (double v) => Math.Log(v));
        TimeOperation(iterations, value, (double v) => Math.Sin(v));

        Console.WriteLine();
    }

    private static void TestFloat()
    {
        float value = (float)BASE_VALUE;
        long iterations = FAST_TYPE_ITERATIONS;
        Console.Write("{0,-10} | ", "float");

        TimeOperation(iterations, value, (float v) => (float)Math.Sqrt(v));
        TimeOperation(iterations, value, (float v) => (float)Math.Log(v));
        TimeOperation(iterations, value, (float v) => (float)Math.Sin(v));

        Console.WriteLine();
    }

    private static void TestDecimal()
    {
        decimal value = (decimal)BASE_VALUE;
        long iterations = DECIMAL_ITERATIONS;
        Console.Write("{0,-10} | ", "decimal");

        TimeOperation(iterations, value, (decimal v) => (decimal)Math.Sqrt((double)v));
        TimeOperation(iterations, value, (decimal v) => (decimal)Math.Log((double)v));
        TimeOperation(iterations, value, (decimal v) => (decimal)Math.Sin((double)v));

        Console.WriteLine();
    }

    private static void TimeOperation<T>(long iterations, T initialValue, Func<T, T> operation)
    {
        T resultHolder = initialValue;
        Stopwatch stopwatch = new Stopwatch();

        for (int i = 0; i < 1000; i++)
        {
            resultHolder = operation(initialValue);
        }

        stopwatch.Start();
        for (long i = 0; i < iterations; i++)
        {
            resultHolder = operation(initialValue);
        }
        stopwatch.Stop();

        Console.Write("{0,-16:F2} | ", stopwatch.Elapsed.TotalMilliseconds);

        if (resultHolder != null)
        {
            return;
        }
    }
}