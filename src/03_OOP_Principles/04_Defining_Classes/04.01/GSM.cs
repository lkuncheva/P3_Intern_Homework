using _04._01;
using System.Text;

public class GSM
{
    private string model;
    private string manufacturer;
    private double? price;
    private string owner;
    private Battery phoneBattery;
    private Display phoneDisplay;

    public List<Call> CallHistory { get; set; }

    private static readonly GSM iPhone4S = new GSM
    {
        Model = "iPhone 4S",
        Manufacturer = "Apple",
        Price = 199.99,
        Owner = "Steve Jobs",
        PhoneBattery = new Battery("616 - 0582", 200, 8, BatteryType.LiIon),
        PhoneDisplay = new Display(3.5, 16000000)
    };

    // --- Properties ---

    public required string Model
    {
        get { return model; }
        set { model = value; }
    }

    public required string Manufacturer
    { 
        get { return manufacturer; }
        set { manufacturer = value; }
    }

    public double? Price
    { 
        get { return price; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }

            price = value;
        }
    }

    public string Owner 
    { 
        get { return owner; } 
        set { owner = value; } 
    }

    public Battery PhoneBattery 
    {
        get { return phoneBattery; }
        set { phoneBattery = value; }
    }
    public Display PhoneDisplay
    {
        get { return phoneDisplay; }
        set { phoneDisplay = value; }
    }

    public static GSM IPhone4S
    {
        get { return iPhone4S; }
    }

    // --- Constructors ---

    public GSM() 
    {
        this.CallHistory = new List<Call>();
    }

    public GSM(string model, string manufacturer, double? price, string owner, Battery battery, Display display)
    {
        this.Model = model;
        this.Manufacturer = manufacturer;
        this.Price = price;
        this.Owner = owner;
        this.PhoneBattery = battery;
        this.PhoneDisplay = display;
        this.CallHistory = new List<Call>();
    }

    public GSM(string model, string manufacturer) : this(model, manufacturer, null, null, new Battery(), new Display()) 
    {
        this.CallHistory = new List<Call>();
    }

    // --- Methods ---

    public void AddCall(string phoneNumber, int duration)
    {
        Call newCall = new Call(phoneNumber, duration);
        this.CallHistory.Add(newCall);
    }

    public void DeleteCall(Call call)
    {
        this.CallHistory.Remove(call);
    }

    public void ClearCallHistory()
    {
        this.CallHistory.Clear();
    }

    public double CallsPrice(double pricePerMinute)
    {
        double price = 0;
        foreach (Call call in this.CallHistory) 
        {
            price += pricePerMinute * (double)call.DurationInSeconds / 60;
        }

        return price;
    }

    public override string ToString()
    {
        string callHistoryString = string.Empty;
        if (this.CallHistory != null && this.CallHistory.Count > 0)
        {
            callHistoryString = "\n--- Call History ---";
            foreach (var call in this.CallHistory)
            {
                callHistoryString += $"\n{call}";
            }
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("\n--- Phone Details ---");
        sb.AppendLine("------------------------------------");
        sb.AppendLine($"Model: {Model}");
        sb.AppendLine($"Manufacturer: {Manufacturer}");
        sb.AppendLine($"Price: {(Price.HasValue ? Price.Value.ToString("C") : "N/A")}");
        sb.AppendLine($"Owner: {(string.IsNullOrWhiteSpace(Owner) ? "N/A" : Owner)}");

        if (PhoneBattery != null)
        {
            sb.AppendLine(PhoneBattery.ToString()); 
        }

        if (PhoneDisplay != null)
        {
            sb.AppendLine(PhoneDisplay.ToString()); 
        }

        sb.AppendLine(callHistoryString);
        sb.AppendLine("------------------------------------");

        return sb.ToString();
    }
}