namespace Methods;
public class Methods
{
    public static double CalculateTriangleArea(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
        {
            throw new ArgumentException("All side lengths must be positive numbers.");
        }

        if (!((a + b > c) && (a + c > b) && (b + c > a)))
        {
            throw new ArgumentException("The provided sides cannot form a valid triangle (triangle inequality violation).");
        }

        double semiPerimeter = (a + b + c) / 2;
        double area = Math.Sqrt(semiPerimeter * (semiPerimeter - a) * (semiPerimeter - b) * (semiPerimeter - c));

        return area;
    }

    public static string ConvertDigitToWord(int number)
    {
        switch (number)
        {
            case 0: return "zero";
            case 1: return "one";
            case 2: return "two";
            case 3: return "three";
            case 4: return "four";
            case 5: return "five";
            case 6: return "six";
            case 7: return "seven";
            case 8: return "eight";
            case 9: return "nine";
            default:
                throw new ArgumentOutOfRangeException(nameof(number),
                        "Input must be a single digit between 0 and 9.");
        }
    }

    public static int FindMaxElement(params int[] elements)
    {
        if (elements == null || elements.Length == 0)
        {
            throw new ArgumentException("The array of elements cannot be null or empty.", nameof(elements));
        }

        int maxElement = elements[0];

        for (int i = 1; i < elements.Length; i++)
        {
            if (elements[i] > maxElement)
            {
                maxElement = elements[i];
            }
        }

        return maxElement;
    }

    public static string FormatNumber(double number, string format)
    {
        switch (format)
        {
            case "f":
                return string.Format("{0:F2}", number);
            case "%":
                return string.Format("{0:P0}", number);
            case "r":
                return string.Format("{0,8}", number);
            default:
                throw new ArgumentException($"Invalid format specifier: '{format}'. Must be 'f', '%', or 'r'.", nameof(format));
        }
    }

    public static double CalculateDistance(double x1, double y1, double x2, double y2)
    {
        double deltaX = x2 - x1;
        double deltaY = y2 - y1;

        double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

        return distance;
    }

    public static bool IsHorizontal(double y1, double y2)
    {
        return y1 == y2;
    }

    public static bool IsVertical(double x1, double x2)
    {
        return x1 == x2;
    }
}