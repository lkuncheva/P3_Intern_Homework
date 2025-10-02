namespace AnimalHierarchy;

public static class AnimalAgeCalculator
{
    public static double CalculateAverageAge<T>(IEnumerable<T> animals) where T : Animal
    {
        if (!animals.Any())
        {
            return 0.0;
        }

        return animals.Average(a => a.Age);
    }
}