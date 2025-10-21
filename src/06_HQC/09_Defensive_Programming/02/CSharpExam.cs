namespace ExceptionsHomework;

public class CSharpExam : Exam
{
    public const int MinScore = 0;
    public const int MaxScore = 100;

    public int Score { get; private set; }

    public CSharpExam(int score)
    {
        if (score < MinScore || score > MaxScore)
        {
            throw new ArgumentOutOfRangeException(nameof(score), $"Score must be between {MinScore} and {MaxScore} inclusive.");
        }

        Score = score;
    }

    public override ExamResult Check()
    {
        return new ExamResult(Score, MinScore, MaxScore, "Exam results calculated by score.");
    }
}