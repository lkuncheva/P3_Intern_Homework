namespace ExceptionsHomework;

public class Student
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public IList<Exam>? Exams { get; set; }

    public Student(string firstName, string lastName, IList<Exam>? exams = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentNullException("First name is mandatory and cannot be null, empty, or whitespace.", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentNullException("Last name is mandatory and cannot be null, empty, or whitespace.", nameof(lastName));
        }

        FirstName = firstName;
        LastName = lastName;
        Exams = exams;
    }

    public IList<ExamResult> CheckExams()
    {
        if (Exams == null)
        {
            throw new ArgumentNullException("The student's exam list is null. Cannot check exams.", nameof(Exams));
        }

        if (Exams.Count == 0)
        {
            return new List<ExamResult>();
        }

        IList<ExamResult> results = new List<ExamResult>();
        for (int i = 0; i < this.Exams.Count; i++)
        {
            results.Add(Exams[i].Check());
        }

        return results;
    }

    public double CalcAverageExamResultInPercents()
    {
        if (this.Exams == null)
        {
            throw new ArgumentNullException("Cannot calculate average on a null exam list.", nameof(Exams));
        }

        if (this.Exams.Count == 0)
        {
            return 0.0;
        }

        IList<ExamResult> examResults = CheckExams();
        double[] examScore = new double[examResults.Count];

        for (int i = 0; i < examResults.Count; i++)
        {
            double score = examResults[i].Grade - examResults[i].MinGrade;
            double range = examResults[i].MaxGrade - examResults[i].MinGrade;

            if (range == 0)
            {
                throw new InvalidOperationException("Exam result range is zero, which prevents percentage calculation.");
            }

            examScore[i] = score / range;
        }

        return examScore.Average();
    }
}