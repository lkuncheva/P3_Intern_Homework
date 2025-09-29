namespace StudentsAndWorkers
{
    public abstract class Human
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public Human(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}