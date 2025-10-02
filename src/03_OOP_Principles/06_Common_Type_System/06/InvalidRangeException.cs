namespace _06;

public class InvalidRangeException<T> : Exception where T : IComparable<T>
{
    public T Start { get; }
    public T End { get; }

    public InvalidRangeException(string message, T start, T end)
        : base(message)
    {
        Start = start;
        End = end;
    }

    public override string Message
    {
        get
        {
            return $"{base.Message} Valid range is: [{Start} ... {End}].";
        }
    }
}