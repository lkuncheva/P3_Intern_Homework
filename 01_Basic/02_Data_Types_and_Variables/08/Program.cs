using System;

class Program
{
    static void Main(string[] args)
    {
        char a = '\u00A9';

        const int height = 4; // Rows
        string spaces; // Spaces before the first character in each row
        string innerSpaces; // Spaces in between the two outer characters on 2nd and 3rd row

        for (int i  = 0; i < height; i++ )
        {

            spaces = new string(' ', height - i);
            Console.Write(spaces);

            if ( i == 0)
            {
                //On the first row we only have 1 character
                Console.WriteLine(a);
            }
            else if ( i == height - 1)
            {
                //On the last one, we have an alternation of scpaces and caracters for the whole row
                for (int j = 0; j <= i; j++)
                {
                    Console.Write(a + " ");
                }
            }
            else
            {
                //On the remaining ones, we have 2 characters and we calculate the spaces/gap in between them
                innerSpaces = new string(' ', i * 2 - 1);
                Console.WriteLine(a + innerSpaces + a);
            }
        }
    }
}