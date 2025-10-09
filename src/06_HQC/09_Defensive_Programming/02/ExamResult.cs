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
            throw new ArgumentOutOfRangeException("Grade cannot be negative", nameof(grade));
        }

        if (minGrade < 0)
        {
            throw new ArgumentOutOfRangeException("Min grade cannot be negative", nameof(minGrade));
        }

        if (maxGrade <= minGrade)
        {
            throw new ArgumentOutOfRangeException("Max grade cannot be less than the min grade", nameof(maxGrade), nameof(minGrade));
        }

        if (string.IsNullOrWhiteSpace(comments))
        {
            throw new ArgumentNullException("Comments are required for an exam result.", nameof(comments));
        }

        Grade = grade;
        MinGrade = minGrade;
        MaxGrade = maxGrade;
        Comments = comments;
    }
}