namespace AnimalHierarchy
{
    public class Cat : Animal
    {
        public Cat(string name, int age, Sex sex) : base(name, age, sex) { }

        public override string ProduceSound()
        {
            return "Meow!";
        }
    }
}