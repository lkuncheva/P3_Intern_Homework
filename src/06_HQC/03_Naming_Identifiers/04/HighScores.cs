namespace MineSweeper;

public static class HighScores
{
    public static void UpdateHighScores(List<PlayerScore> highScores, PlayerScore newScore)
    {
        highScores.Add(newScore);

        List<PlayerScore> sortedScores = highScores
            .OrderByDescending(s => s.Score)
            .Take(5)
            .ToList();

        highScores.Clear();
        highScores.AddRange(sortedScores);
    }

    public static void DisplayHighScores(List<PlayerScore> highScores)
    {
        Console.WriteLine("\n------- High Scores -------");
        if (highScores.Count == 0)
        {
            Console.WriteLine("No scores recorded yet.");

            return;
        }

        for (int i = 0; i < highScores.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {highScores[i].Nickname} - {highScores[i].Score} points");
        }

        Console.WriteLine("---------------------------\n");
    }
}