namespace InheritanceAndPolymorphism;

public class Program
{
    public static void RunValidExamples()
    {
        Console.WriteLine("--- Running Valid Course Examples ---");

        var localCourse = new LocalCourse("Databases");
        Console.WriteLine(localCourse);

        localCourse.Lab = "Enterprise";
        Console.WriteLine(localCourse);

        localCourse.Students.Add("Peter");
        localCourse.Students.Add("Maria");
        Console.WriteLine(localCourse);

        localCourse.TeacherName = "Svetlin Nakov";
        localCourse.Students.Add("Milena");
        localCourse.Students.Add("Todor");
        Console.WriteLine(localCourse);

        var offsiteCourse = new OffsiteCourse(
            "PHP and WordPress Development",
            "Mario Peshev",
            new List<string>() { "Thomas", "Ani", "Steve" },
            "Plovdiv");

        Console.WriteLine(offsiteCourse);
    }

    public static void RunInvalidExamples()
    {
        Console.WriteLine("\n--- Demonstration of Constraint Violations (Exceptions) ---");

        Console.WriteLine("Attempting to create a course with a null name...");
        Course mandatoryNameViolation = new LocalCourse(null);

        // LocalCourse validCourse = new LocalCourse("Test Course");
        // Console.WriteLine("Attempting to set Lab property to whitespace...");
        // validCourse.Lab = " ";
    }

    public static void Main()
    {
        try
        {
            RunValidExamples();
            // RunInvalidExamples(); 
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[VALIDATION ERROR] Program terminated due to incorrect data: {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n[RUNTIME ERROR] An unexpected error occurred: {ex.Message}");
            Console.ResetColor();

            throw;
        }
    }
}