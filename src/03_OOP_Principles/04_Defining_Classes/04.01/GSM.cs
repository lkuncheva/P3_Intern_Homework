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
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Model cannot be empty.");
            }
            model = value;
        }
    }
    public required string Manufacturer
    { 
        get { return manufacturer; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Manufacturer cannot be empty.");
            }
            manufacturer = value;
        }
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

    public GSM() { }
    public GSM(string model, string manufacturer, double? price, string owner, Battery battery, Display display)
    {
        this.Model = model;
        this.Manufacturer = manufacturer;
        this.Price = price;
        this.Owner = owner;
        this.PhoneBattery = battery;
        this.PhoneDisplay = display;
    }

    public GSM(string model, string manufacturer) : this(model, manufacturer, null, null, new Battery(), new Display()) { }

    public GSM(string model, string manufacturer, double? price) : this(model, manufacturer, price, null, new Battery(), new Display()) { }

    public GSM(string model, string manufacturer, string owner) : this(model, manufacturer, null, owner, new Battery(), new Display()) { }

    public GSM(string model, string manufacturer, Battery battery) : this(model, manufacturer, null, null, battery, new Display()) { }

    public GSM(string model, string manufacturer, Display display) : this(model, manufacturer, null, null, new Battery(), display) { }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
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
        sb.AppendLine("------------------------------------");
        return sb.ToString();
    }
}