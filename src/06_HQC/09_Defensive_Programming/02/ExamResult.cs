namespace ExceptionsHomework;

public class ExamResult
{
    public int Grade { get; private set; }
    public int MinGrade { get; private set; }
    public int MaxGrade { get; private set; }
    public string Comments { get; private set; }

    public ExamResult(int grade, int minGrade, int maxGrade, string comments)
    {
        if (grade < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(grade), "Grade cannot be negative");
        }

        if (minGrade < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minGrade), "Min grade cannot be negative");
        }

        if (maxGrade <= minGrade)
        {
            throw new ArgumentOutOfRangeException(nameof(maxGrade), nameof(minGrade), "Max grade cannot be less than the min grade");
        }

        if (comments == null || string.IsNullOrWhiteSpace(comments))
        {
            throw new ArgumentNullException(nameof(comments),
                                            "Comments are required for an exam result and cannot be empty or consist only of whitespace.");
        }

        Grade = grade;
        MinGrade = minGrade;
        MaxGrade = maxGrade;
        Comments = comments;
    }
}