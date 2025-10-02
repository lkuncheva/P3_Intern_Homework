namespace _04._02;

[AttributeUsage(
AttributeTargets.Class |
AttributeTargets.Struct |
AttributeTargets.Interface |
AttributeTargets.Enum |
AttributeTargets.Method,
AllowMultiple = false)]
public class VersionAttribute : Attribute
{
    public string Version { get; }

    public VersionAttribute(string version)
    {
        if (string.IsNullOrWhiteSpace(version))
        {
            throw new ArgumentException("Version cannot be null or empty.", nameof(version));
        }

        if (!System.Text.RegularExpressions.Regex.IsMatch(version, @"^\d+\.\d+$"))
        {
            Console.WriteLine($"Warning: Version format '{version}' might be incorrect. Expected format: major.minor.");
        }

        this.Version = version;
    }
}