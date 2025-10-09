using System.Diagnostics;

namespace PerformanceComparison;

public class Program
{
    private const long FAST_TYPE_ITERATIONS = 2_000_000_000;
    private const long DECIMAL_ITERATIONS = 200_000_000;

    public static void Main(string[] args)
    {
        Console.WriteLine("--- Arithmetic Operation Performance Comparison ---");
        Console.WriteLine($"Fast Type Iterations: {FAST_TYPE_ITERATIONS:N0}");
        Console.WriteLine($"Decimal Iterations:   {DECIMAL_ITERATIONS:N0}\n");

        Console.WriteLine(
            "{0,-10} | {1,-14} | {2,-14} | {3,-14} | {4,-14} | {5,-14}",
            "Type", "Add (+)", "Subtract (-)", "Increment (++)", "Multiply (*)", "Divide (/)"
        );
        Console.WriteLine(new string('-', 90));

        TestType<int>("int", FAST_TYPE_ITERATIONS);
        TestType<long>("long", FAST_TYPE_ITERATIONS);
        TestType<float>("float", FAST_TYPE_ITERATIONS);
        TestType<double>("double", FAST_TYPE_ITERATIONS);
        TestType<decimal>("decimal", DECIMAL_ITERATIONS);

        Console.WriteLine(new string('-', 90));
        Console.WriteLine("\nAll times are in milliseconds (ms). Lower is better.");
        Console.WriteLine("P.S. Decimal is always the slowest because it handles a lot more precision.");
        Console.WriteLine("P.P.S. Decimal multiplication uses a tiny number to prevent the result from overflowing.");
    }

    private static void TestType<T>(string typeName, long iterations)
    {
        T secondValue = GetSecondValue<T>();

        T multiplyValue = secondValue;

        if (typeof(T) == typeof(decimal))
        {
            multiplyValue = (T)(object)0.00000001M;
        }

        Console.Write("{0,-10} | ", typeName);

        TimeOperation(iterations, (T val) => Add(val, secondValue));
        TimeOperation(iterations, (T val) => Subtract(val, secondValue));
        TimeOperation(iterations, (T val) => Increment(val));
        TimeOperation(iterations, (T val) => Multiply(val, multiplyValue));
        TimeOperation(iterations, (T val) => Divide(val, secondValue));

        Console.WriteLine();
    }

    private static void TimeOperation<T>(long iterations, Func<T, T> operation)
    {
        T currentValue = GetInitialValue<T>();
        Stopwatch stopwatch = new Stopwatch();

        for (int i = 0; i < 1000; i++)
        {
            currentValue = operation(currentValue);
        }

        stopwatch.Start();
        for (long i = 0; i < iterations; i++)
        {
            currentValue = operation(currentValue);
        }
        stopwatch.Stop();

        Console.Write("{0,-14:F2} | ", stopwatch.Elapsed.TotalMilliseconds);

        if (currentValue != null)
        {
            return;
        }
    }

    private static T GetInitialValue<T>()
    {
        if (typeof(T) == typeof(int)) return (T)(object)1;
        if (typeof(T) == typeof(long)) return (T)(object)1L;
        if (typeof(T) == typeof(float)) return (T)(object)1.000001f;
        if (typeof(T) == typeof(double)) return (T)(object)1.000000000001;
        if (typeof(T) == typeof(decimal)) return (T)(object)1.0000000000000000000000000001M;
        return default;
    }

    private static T GetSecondValue<T>()
    {
        if (typeof(T) == typeof(int)) return (T)(object)2;
        if (typeof(T) == typeof(long)) return (T)(object)2L;
        if (typeof(T) == typeof(float)) return (T)(object)2.0f;
        if (typeof(T) == typeof(double)) return (T)(object)2.0;
        if (typeof(T) == typeof(decimal)) return (T)(object)2M;
        return default;
    }

    private static T Add<T>(T a, T b)
    {
        if (a is int aInt && b is int bInt) return (T)(object)(aInt + bInt);
        if (a is long aLong && b is long bLong) return (T)(object)(aLong + bLong);
        if (a is float aFloat && b is float bFloat) return (T)(object)(aFloat + bFloat);
        if (a is double aDouble && b is double bDouble) return (T)(object)(aDouble + bDouble);
        if (a is decimal aDecimal && b is decimal bDecimal) return (T)(object)(aDecimal + bDecimal);
        throw new NotSupportedException("Type not supported for Addition.");
    }

    private static T Subtract<T>(T a, T b)
    {
        if (a is int aInt && b is int bInt) return (T)(object)(aInt - bInt);
        if (a is long aLong && b is long bLong) return (T)(object)(aLong - bLong);
        if (a is float aFloat && b is float bFloat) return (T)(object)(aFloat - bFloat);
        if (a is double aDouble && b is double bDouble) return (T)(object)(aDouble - bDouble);
        if (a is decimal aDecimal && b is decimal bDecimal) return (T)(object)(aDecimal - bDecimal);
        throw new NotSupportedException("Type not supported for Subtraction.");
    }

    private static T Increment<T>(T a)
    {
        if (a is int aInt) return (T)(object)(aInt + 1);
        if (a is long aLong) return (T)(object)(aLong + 1L);
        if (a is float aFloat) return (T)(object)(aFloat + 1.0f);
        if (a is double aDouble) return (T)(object)(aDouble + 1.0);
        if (a is decimal aDecimal) return (T)(object)(aDecimal + 1M);
        throw new NotSupportedException("Type not supported for Incrementation.");
    }

    private static T Multiply<T>(T a, T b)
    {
        if (a is int aInt && b is int bInt) return (T)(object)(aInt * bInt);
        if (a is long aLong && b is long bLong) return (T)(object)(aLong * bLong);
        if (a is float aFloat && b is float bFloat) return (T)(object)(aFloat * bFloat);
        if (a is double aDouble && b is double bDouble) return (T)(object)(aDouble * bDouble);
        if (a is decimal aDecimal && b is decimal bDecimal) return (T)(object)(aDecimal * bDecimal);
        throw new NotSupportedException("Type not supported for Multiplication.");
    }

    private static T Divide<T>(T a, T b)
    {
        if (a is int aInt && b is int bInt) return (T)(object)(aInt / bInt);
        if (a is long aLong && b is long bLong) return (T)(object)(aLong / bLong);
        if (a is float aFloat && b is float bFloat) return (T)(object)(aFloat / bFloat);
        if (a is double aDouble && b is double bDouble) return (T)(object)(aDouble / bDouble);
        if (a is decimal aDecimal && b is decimal bDecimal) return (T)(object)(aDecimal / bDecimal);
        throw new NotSupportedException("Type not supported for Division.");
    }
}