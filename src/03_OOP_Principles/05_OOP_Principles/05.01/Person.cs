namespace School
{ 
    public abstract class Person
    {
        private readonly string name;

        public string Name => name;

        public Person(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }
                
            this.name = name;
        }
    }
}