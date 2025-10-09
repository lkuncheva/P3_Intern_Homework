public class Class123
{
    private const int MAX_COUNT = 6;
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