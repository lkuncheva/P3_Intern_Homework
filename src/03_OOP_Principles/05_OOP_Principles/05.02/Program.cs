namespace StudentsAndWorkers;
public class Program
{
    public static void Main()
    {
        List<Student> students = new List<Student>
        {
            new Student("Alice", "Smith", 85),
            new Student("Bob", "Johnson", 62),
            new Student("Charlie", "Brown", 98),
            new Student("Diana", "Miller", 77),
            new Student("Evan", "Wilson", 90),
            new Student("Fiona", "Davis", 55),
            new Student("George", "Garcia", 88),
            new Student("Hannah", "Rodriguez", 70),
            new Student("Ian", "Martinez", 93),
            new Student("Julia", "Hernandez", 65)
        };

        var sortedStudents = students
            .OrderBy(s => s.Grade)
            .ToList();

        Console.WriteLine("--- Sorted Students (by Grade ASC) ---");
        foreach (var student in sortedStudents)
        {
            Console.WriteLine(student);
        }

        Console.WriteLine("\n" + new string('-', 40) + "\n");

        List<Worker> workers = new List<Worker>
        {
            new Worker("Wanda", "White", 800m, 8),
            new Worker("Xavier", "King", 1200m, 6),
            new Worker("Yara", "Scott", 900m, 7),
            new Worker("Zane", "Adams", 1500m, 5),
            new Worker("Victor", "Baker", 750m, 8),
            new Worker("Quinn", "Carter", 1000m, 7.5),
            new Worker("Paul", "Dixon", 1100m, 6.5),
            new Worker("Owen", "Evans", 950m, 8),
            new Worker("Noah", "Foster", 1300m, 7),
            new Worker("Mike", "Green", 1600m, 4)
        };

        var sortedWorkers = workers
            .OrderByDescending(w => w.MoneyPerHour())
            .ToList();

        Console.WriteLine("--- Sorted Workers (by Money Per Hour DESC) ---");
        foreach (var worker in sortedWorkers)
        {
            Console.WriteLine(worker);
        }

        Console.WriteLine("\n" + new string('-', 40) + "\n");

        List<Human> mergedHumans = new List<Human>();
        mergedHumans.AddRange(students);
        mergedHumans.AddRange(workers);

        var sortedHumans = mergedHumans
            .OrderBy(h => h.FirstName)
            .ThenBy(h => h.LastName)
            .ToList();

        Console.WriteLine("--- Merged and Sorted Humans (by First/Last Name) ---");
        foreach (var human in sortedHumans)
        {
            Console.WriteLine(human);
        }
    }
}