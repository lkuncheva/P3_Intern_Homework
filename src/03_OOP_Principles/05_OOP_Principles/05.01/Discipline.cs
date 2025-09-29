namespace School
{
    public class Discipline
    {
        private readonly string name;
        private readonly int lecturesCount;
        private readonly int exercisesCount;
        private string comments;

        public string Name => name;
        public int LecturesCount => lecturesCount;
        public int ExercisesCount => exercisesCount;

        public string Comments { get => comments; set => comments = value; }

        public Discipline(string name, int lecturesCount, int exercisesCount)
        {
            this.name = name;
            this.lecturesCount = lecturesCount;
            this.exercisesCount = exercisesCount;
        }
    }
}