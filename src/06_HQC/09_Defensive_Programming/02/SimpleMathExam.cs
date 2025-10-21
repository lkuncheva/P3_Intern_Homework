namespace ExceptionsHomework;

public class SimpleMathExam : Exam
{
    public const int MaxProblems = 10;

    public int ProblemsSolved { get; private set; }

    public SimpleMathExam(int problemsSolved)
    {
        if (problemsSolved < 0 || problemsSolved > MaxProblems)
        {
            throw new ArgumentOutOfRangeException(nameof(problemsSolved), $"Problems solved must be between 0 and {MaxProblems}.");
        }

        ProblemsSolved = problemsSolved;
    }

    public override ExamResult Check()
    {
        if (ProblemsSolved == 0)
        {
            return new ExamResult(2, 2, 6, "Fail: Zero problems solved.");
        }
        else if (ProblemsSolved == 1)
        {
            return new ExamResult(3, 2, 6, "Poor: Only one problem solved.");
        }
        else if (ProblemsSolved >= 2 && ProblemsSolved <= 3)
        {
            return new ExamResult(4, 2, 6, "Satisfactory: Two or three problems solved.");
        }
        else if (ProblemsSolved >= 4 && ProblemsSolved <= 6)
        {
            return new ExamResult(5, 2, 6, "Good: Half or more problems solved.");
        }
        else
        {
            return new ExamResult(6, 2, 6, "Excellent: Seven or more problems solved.");
        }
    }
}