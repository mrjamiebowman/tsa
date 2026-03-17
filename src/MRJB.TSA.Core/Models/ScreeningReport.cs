namespace MRJB.TSA.Core.Models;

#nullable enable

public class ScreeningReport
{
    public bool Pass { get; set; }

    public List<ConfigurationReport> Configuration = new();
}

public class ConfigurationReport
{
    public bool Pass { get; set; }

    public string? Name { get; set; }

    public string? Message { get; set; }

    public List<ConfigurationPropertyReport> Properties = new();
}

public class ConfigurationPropertyReport
{
    public string? Name { get; set; }

    public bool Pass { get; set; }

    public string? Message { get; set; }

    public bool? Required { get; set; }

    // TYPE (SQL, etc)

    public string? Reason { get; set; }
}

#nullable disable