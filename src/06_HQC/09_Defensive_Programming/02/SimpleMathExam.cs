namespace ExceptionsHomework;

public class SimpleMathExam : Exam
{
    public const int MAX_PROBLEMS = 10;

    public int ProblemsSolved { get; private set; }

    public SimpleMathExam(int problemsSolved)
    {
        if (problemsSolved < 0 || problemsSolved > MAX_PROBLEMS)
        {
            throw new ArgumentOutOfRangeException($"Problems solved must be between 0 and {MAX_PROBLEMS}.",
                                                  nameof(problemsSolved));
        }

        ProblemsSolved = problemsSolved;
    }

    public override ExamResult Check()
    {
        if (ProblemsSolved == 0)
        {
            return new ExamResult(2, 2, 6, "Bad result: nothing done.");
        }
        else if (ProblemsSolved == 1)
        {
            return new ExamResult(4, 2, 6, "Average result: nothing done.");
        }
        else if (ProblemsSolved >= 2 && ProblemsSolved <= MAX_PROBLEMS)
        {
            return new ExamResult(6, 2, 6, "Average result: nothing done.");
        }

        throw new InvalidOperationException("SimpleMathExam is in an invalid state. Cannot check result.");
    }
}