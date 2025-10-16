public class Class123
{
    private const int MaxCount = 6;
    public class InnerClass
    {
        public void InnerClassMethod(bool inputVariable)
        {
            string variableAsString = inputVariable.ToString();
            Console.WriteLine(variableAsString);
        }
    }

    public static void Main()
    {
        var instance = new InnerClass();
        instance.InnerClassMethod(true);
    }
}