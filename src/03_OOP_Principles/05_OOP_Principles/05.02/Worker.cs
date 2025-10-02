namespace StudentsAndWorkers;

public class Worker : Human
{
    private const int WorkDaysPerWeek = 5;

    public decimal WeekSalary { get; private set; }

    public double WorkHoursPerDay { get; private set; }

    public Worker(string firstName, string lastName, decimal weekSalary, double workHoursPerDay)
        : base(firstName, lastName)
    {
        WeekSalary = weekSalary;
        WorkHoursPerDay = workHoursPerDay;
    }

    public decimal MoneyPerHour()
    {
        double totalWeeklyHours = WorkHoursPerDay * WorkDaysPerWeek;

        if (totalWeeklyHours == 0)
        {
            return 0M;
        }

        return WeekSalary / (decimal)totalWeeklyHours;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Salary: {WeekSalary:C}, Hours/Day: {WorkHoursPerDay:F1}, Hourly Rate: {MoneyPerHour():C}";
    }
}