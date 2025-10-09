namespace ExceptionsHomework;

public class CSharpExam : Exam
{
    public const int MIN_SCORE = 0;
    public const int MAX_SCORE = 100;

    public int Score { get; private set; }

    public CSharpExam(int score)
    {
        if (score < MIN_SCORE || score > MAX_SCORE)
        {
            throw new ArgumentOutOfRangeException($"Score must be between {MIN_SCORE} and {MAX_SCORE} inclusive.",
                                                  nameof(score));
        }

        Score = score;
    }

    public override ExamResult Check()
    {
        return new ExamResult(Score, MIN_SCORE, MAX_SCORE, "Exam results calculated by score.");
    }
}