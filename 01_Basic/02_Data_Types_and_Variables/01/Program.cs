using System;

class Program
{
    static void Main(string[] args)
    {
        byte a = 97;
        sbyte b = -115;
        short c = -10000;
        ushort d = 52130;
        int e = 4825932;

        Console.WriteLine(a + ": byte(0 to 255) \n" + 
                          b + ": sbyte(-128 to 127) \n" +
                          c + ": short(-32,768 to 32,767) \n" +
                          d + ": ushort(0 to 65,535) \n" +
                          e + ": int(-2,147,483,648 to 2,147,483,647)");

    }
}
             