using System.Text;

namespace High.Quality.Code.BadExample
{
    public class Bunnies
    {
        static void Main(string[] args)
        {
            var bunnies = new List<Bunny>
            {
                new Bunny { Name = "Leonid", Age = 1, FurType = FurType.NotFluffy },
                new Bunny { Name = "Rasputin", Age = 2, FurType = FurType.ALittleFluffy },
                new Bunny { Name = "Tiberii", Age = 3, FurType = FurType.ALittleFluffy },
                new Bunny { Name = "Neron", Age = 1, FurType = FurType.ALittleFluffy },
                new Bunny { Name = "Klavdii", Age = 3, FurType = FurType.Fluffy },
                new Bunny { Name = "Vespasian", Age = 3, FurType = FurType.Fluffy },
                new Bunny { Name = "Domician", Age = 4, FurType = FurType.FluffyToTheLimit },
                new Bunny { Name = "Tit", Age = 2, FurType = FurType.FluffyToTheLimit }
            };

            var consoleWriter = new ConsoleWriter();
            foreach (var bunny in bunnies)
            {
                bunny.Introduce(consoleWriter);
            }

            const string BunniesFilePath = @"..\..\bunnies.txt";

            using (var streamWriter = new StreamWriter(BunniesFilePath))
            {
                foreach (var bunny in bunnies)
                {
                    streamWriter.WriteLine(bunny.ToString());
                }
            }
        }
    }

    [Serializable]
    public class Bunny
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public FurType FurType { get; set; }

        public void Introduce(IWriter writer)
        {
            writer.WriteLine($"{this.Name} - \"I am {this.Age} years old!\"");
            writer.WriteLine($"{this.Name} - \"And I am {this.FurType.ToString().SplitToSeparateWordsByUppercaseLetter()}\"");
        }

        public override string ToString()
        {
            const int BuilderSize = 200;
            var builder = new StringBuilder(BuilderSize);

            builder.AppendLine($"Bunny name: {this.Name}");
            builder.AppendLine($"Bunny age: {this.Age}");
            builder.AppendLine($"Bunny fur: {this.FurType.ToString().SplitToSeparateWordsByUppercaseLetter()}");

            return builder.ToString();
        }
    }

    public enum FurType
    {
        NotFluffy,
        ALittleFluffy,
        Fluffy,
        FluffyToTheLimit
    }

    public interface IWriter
    {
        void Write(string message);
        void WriteLine(string message);
    }

    public class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }

    public static class StringExtensions
    {
        public static string SplitToSeparateWordsByUppercaseLetter(this string sequence)
        {
            const int ProbableStringMargin = 10;
            var builder = new StringBuilder(sequence.Length + ProbableStringMargin);

            const char SingleWhitespace = ' ';

            foreach (var letter in sequence)
            {
                if (Char.IsUpper(letter))
                {
                    builder.Append(SingleWhitespace);
                }

                builder.Append(letter);
            }

            return builder.ToString().Trim();
        }
    }
}