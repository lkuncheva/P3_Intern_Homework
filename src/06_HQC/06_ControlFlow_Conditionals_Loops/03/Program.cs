bool valueFound = false;

for (int i = 0; i < 100; i++)
{
    Console.WriteLine(array[i]);

    if (i % 10 == 0)
    {
        if (array[i] == expectedValue)
        {
            // Depending on the expected logic of the loop
            // we can just print "Value Found" here and return;
            valueFound = true;
            break;
        }
    }
}

if (valueFound)
{
    Console.WriteLine("Value Found");
}