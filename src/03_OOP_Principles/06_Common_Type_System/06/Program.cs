using _06;

public class Program
{
    public static void Main(string[] args)
    {
        DemonstrateStudentAndCloning();
        Console.WriteLine("\n-------------------------------------------------\n");
        DemonstrateInvalidRangeException();
        Console.WriteLine("\n-------------------------------------------------\n");
        DemonstratePersonClass();
        Console.WriteLine("\n-------------------------------------------------\n");
        DemonstrateBinarySearchTree();
    }

    private static void DemonstrateStudentAndCloning()
    {
        Console.WriteLine(">>> Demonstration of Students and Cloning");

        Student student1 = new Student("Bai", "Ti", "Tosho", "123456789", "Luksozenets 1234",
                                       "0898233041", "baitosho420@gmail.com", "Cyber Security", Specialty.SoftwareEngineering,
                                       University.MIT, Faculty.Engineering);

        Console.WriteLine("\n--- Original Student ---");
        Console.WriteLine(student1.ToString());

        Student clonedStudent = (Student)student1.Clone();

        clonedStudent.LastName = "Ganio";

        Console.WriteLine("\n--- Cloned Student (Modified) ---");
        Console.WriteLine(clonedStudent.ToString());

        Console.WriteLine($"Check Equality (SSN): {student1 == clonedStudent} (Should be True, based on SSN)");
    }

    private static void DemonstrateInvalidRangeException()
    {
        Console.WriteLine(">>> InvalidRangeException<T> Demonstration");

        int minInt = 1, maxInt = 100;
        int testInt = 150;

        try
        {
            if (testInt < minInt || testInt > maxInt)
            {
                throw new InvalidRangeException<int>($"Integer {testInt} is outside the allowed range.", minInt, maxInt);
            }
        }
        catch (InvalidRangeException<int> ex)
        {
            Console.WriteLine($"Caught Int Exception: {ex.Message}");
        }

        DateTime minDate = new DateTime(1980, 1, 1);
        DateTime maxDate = new DateTime(2013, 12, 31);
        DateTime testDate = new DateTime(2025, 1, 1);

        try
        {
            if (testDate.CompareTo(minDate) < 0 || testDate.CompareTo(maxDate) > 0)
            {
                throw new InvalidRangeException<DateTime>($"Date {testDate:d} is outside the allowed range.", minDate, maxDate);
            }
        }
        catch (InvalidRangeException<DateTime> ex)
        {
            Console.WriteLine($"Caught DateTime Exception: {ex.Message}");
        }
    }

    private static void DemonstratePersonClass()
    {
        Console.WriteLine(">>> Person Class (Optional Age)");

        Person p1 = new Person("Baba Ganka", 90);
        Person p2 = new Person("Sasho");
        Person p3 = new Person("Tosho", null);

        Console.WriteLine(p1.ToString());
        Console.WriteLine(p2.ToString());
        Console.WriteLine(p3.ToString());
    }

    private static void DemonstrateBinarySearchTree()
    {
        Console.WriteLine(">>> Binary Search Tree and ICloneable");

        var tree1 = new BinarySearchTree<int>();
        tree1.Add(50);
        tree1.Add(30);
        tree1.Add(70);
        tree1.Add(20);
        tree1.Add(40);
        tree1.Add(60);
        tree1.Add(80);

        Console.WriteLine($"\nOriginal Tree: {tree1.ToString()}");

        Console.WriteLine($"Search for 40: {tree1.Search(40)} (True)");
        Console.WriteLine($"Search for 99: {tree1.Search(99)} (False)");

        var tree2 = (BinarySearchTree<int>)tree1.Clone();

        tree2.Delete(50);
        tree2.Add(99);

        Console.WriteLine($"\nCloned Tree (Modified): {tree2.ToString()}");
        Console.WriteLine($"Original Tree (Unmodified): {tree1.ToString()}");

        var tree3 = new BinarySearchTree<int>();
        tree3.Add(30); tree3.Add(70); tree3.Add(50);

        Console.WriteLine("\n--- Comparison ---");
        Console.WriteLine($"Tree1 == Tree2 (Should be False): {tree1 == tree2}");
        Console.WriteLine($"Tree1 == Tree3 (Should be False): {tree1 == tree3}");

        tree1.Delete(20);
        Console.WriteLine($"Tree1 After Deleting 20: {tree1.ToString()}");
    }
}