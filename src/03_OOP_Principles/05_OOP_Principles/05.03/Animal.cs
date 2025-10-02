namespace AnimalHierarchy;

public abstract class Animal : ISound
{
    public string Name { get; private set; }

    public int Age { get; private set; }

    public Sex Sex { get; private set; }

    protected Animal(string name, int age, Sex sex)
    {
        Name = name;
        Age = age;
        Sex = sex;
    }

    public abstract string ProduceSound();

    public override string ToString()
    {
        return $"{GetType().Name} - Name: {Name}, Age: {Age}, Sex: {Sex}";
    }
}