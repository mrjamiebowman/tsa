namespace MRJB.TSA.Core.Domain.Models;

public class ConfigurationEntry
{
    public string Assembly { get; set; }

    public string ClassName { get; set; }

    public List<ConfigurationProperty> Properties { get; set; } = new();
}
