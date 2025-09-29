namespace AnimalHierarchy
{
    public class Frog : Animal
    {
        public Frog(string name, int age, Sex sex) : base(name, age, sex) { }


        public override string ProduceSound()
        {
            return "Ribbit!";
        }
    }
}