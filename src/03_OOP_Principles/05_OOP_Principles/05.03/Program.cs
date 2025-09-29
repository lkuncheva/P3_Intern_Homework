namespace AnimalHierarchy
{
    public class Program
    {
        public static void Main()
        {
            List<Dog> dogs = new List<Dog>
            {
                new Dog("Sir Barks-a-Lot", 5, Sex.Male),
                new Dog("Mary Puppins", 2, Sex.Female),
                new Dog("Bark Twain", 8, Sex.Male),
                new Dog("Miss Wiggles", 4, Sex.Female)
            };

            List<Frog> frogs = new List<Frog>
            {
                new Frog("Shrek", 1, Sex.Male),
                new Frog("Karen", 2, Sex.Female),
                new Frog("Hoppy", 1, Sex.Female)
            };

            List<Kitten> kittens = new List<Kitten>
            {
                new Kitten("Meowly Cyrus", 0),
                new Kitten("Twinkle Toes", 1),
                new Kitten("Admiral Turbo Meowington", 0)
            };

            List<Tomcat> tomcats = new List<Tomcat>
            {
                new Tomcat("Bandit Whiskers", 7),
                new Tomcat("Cat Damon", 5)
            };

            Dictionary<string, IEnumerable<Animal>> animalGroups = new Dictionary<string, IEnumerable<Animal>>
            {
                { "Dogs", dogs },
                { "Frogs", frogs },
                { "Kittens", kittens },
                { "Tomcats", tomcats }
            };

            Console.WriteLine("--- Animal Age Statistics ---");

            foreach (var group in animalGroups)
            {
                double avgAge = AnimalAgeCalculator.CalculateAverageAge(group.Value);

                Console.WriteLine($"\nKind: {group.Key}");
                Console.WriteLine($"\tTotal Count: {group.Value.Count()}");
                Console.WriteLine($"\tAverage Age: {avgAge:F2} years");
            }

            Console.WriteLine("\n" + new string('-', 30) + "\n");
            Console.WriteLine("--- Demonstration of Individual Animals ---");

            List<Animal> allAnimals = animalGroups.Values.SelectMany(x => x).ToList();
            foreach (var animal in allAnimals.OrderBy(a => a.GetType().Name))
            {
                Console.WriteLine(animal);
            }
        }
    }
}