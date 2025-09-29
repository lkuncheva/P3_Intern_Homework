namespace StudentsAndWorkers
{
    public class Student : Human
    {
        public int Grade { get; private set; }

        public Student(string firstName, string lastName, int grade) : base(firstName, lastName)
        {
            if (grade <= 0)
            {
                throw new ArgumentException("Grade must be positive.");
            }

            Grade = grade;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Grade: {Grade}";
        }
    }
}