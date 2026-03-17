namespace MRJB.TSA.Core.Models;

#nullable enable

public class ConfigurationEntry
{
    public string Assembly { get; set; }

    public string ClassName { get; set; }

    public Type Type { get; set; }

    public List<ConfigurationProperty> Properties { get; set; } = new();
}

#nullable disable