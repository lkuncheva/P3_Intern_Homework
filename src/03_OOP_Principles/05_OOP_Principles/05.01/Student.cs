namespace School
{
    public class Student : Person
    {
        private readonly int classNumber; 
        private string comments;

        public int ClassNumber => classNumber;

        public string Comments { get => comments; set => comments = value; }

        public Student(string name, int classNumber) : base(name)
        {
            if (classNumber <= 0)
            {
                throw new ArgumentException("Class number must be positive.");
            }

            this.classNumber = classNumber;
        }
    }
}