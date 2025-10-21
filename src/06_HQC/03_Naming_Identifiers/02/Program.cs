class MainClass
{
    enum Sex
    { 
        Male,
        Female
    };
    class Person
    {
        public Sex Sex { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public void CreatePerson(int age)
    {
        var newPerson = new Person();
        newPerson.Age = age;

        if (age % 2 == 0)
        {
            newPerson.Name = "Батката";
            newPerson.Sex = Sex.Male;
        }
        else
        {
            newPerson.Name = "Мацето";
            newPerson.Sex = Sex.Female;
        }
    }
}
