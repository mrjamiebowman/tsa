using System;

namespace TerribleSettingsAuditor.Abstractions.Attribute;

/// <summary>
///  BagggeCart, is used to generate configuration files.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class BaggageCart : System.Attribute
{
    public string Name { get; set; }

    public string DefaultFileName { get; set; } = "appsettings.tsa.json";

    public BaggageCart(string name)
    {
        Name = name;
    }

    public BaggageCart(string name, string fileName)
    {
        Name = name;
        DefaultFileName = fileName;
    }
}
