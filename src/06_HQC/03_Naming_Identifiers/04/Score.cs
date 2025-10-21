namespace MineSweeper;
public class PlayerScore
{
    public string Nickname { get; set; }
    public int Score { get; set; }

    public PlayerScore() 
    {
        Nickname = "N/A";
        Score = 0;
    }

    public PlayerScore(string name, int score)
    {
        Nickname = name;
        Score = score;
    }
}