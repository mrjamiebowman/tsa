namespace MRJB.TSA.Core.Domain.Models;

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

    public List<ConfigurationPropertyReports> Properties = new();
}

public class ConfigurationPropertyReports
{
    public string? Name { get; set; }

    public bool Pass { get; set; }

    // TYPE (SQL, etc)

    public string? Reason { get; set; }
}

#nullable disable