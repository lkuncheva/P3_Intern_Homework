namespace AnimalHierarchy
{
    public class Tomcat : Cat
    {
        public Tomcat(string name, int age)
        : base(name, age, Sex.Male) { }

        public override string ProduceSound()
        {
            return "ROWR! (Deep growl-meow)";
        }
    }
}