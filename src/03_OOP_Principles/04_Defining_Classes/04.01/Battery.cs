namespace _04._01;

public class Battery
{
    private string? model;
    private double? hoursIdle;
    private double? hoursTalk;

    public string? Model
    {
        get { return model; }
        set { model = value; }
    }

    public double? HoursIdle
    { 
        get { return hoursIdle; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Battery hours idle cannot be negative.");
            }

            hoursIdle = value;
        }
    }

    public double? HoursTalk
    { 
        get { return hoursTalk; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Battery hours talk cannot be negative.");
            }

            hoursTalk = value;
        }
    }

    public BatteryType BatteryType { get; set; }

    public Battery()
    {
        this.Model = null;
        this.HoursIdle = null;
        this.HoursTalk = null;
        this.BatteryType = BatteryType.Unknown;
    }

    public Battery(string? model) : this(model, null, null, BatteryType.Unknown) { }

    public Battery(string? model, double? hoursIdle, double? hoursTalk, BatteryType type)
    {
        this.Model = model;
        this.HoursIdle = hoursIdle;
        this.HoursTalk = hoursTalk;
        this.BatteryType = type;
    }

    public override string ToString()
    {
        return $"Battery: Model = {Model ?? "N/A"}, Type = {BatteryType}, Idle = {HoursIdle ?? 0}h, Talk = {HoursTalk ?? 0}h";
    }
}