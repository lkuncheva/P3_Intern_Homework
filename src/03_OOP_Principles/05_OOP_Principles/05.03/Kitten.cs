namespace AnimalHierarchy
{
    public class Kitten : Cat
    {
        public Kitten(string name, int age)
        : base(name, age, Sex.Female) { }

        public override string ProduceSound()
        {
            return "Mew! Mew! (Small meow)";
        }
    }
}
