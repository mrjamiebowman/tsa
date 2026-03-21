using System;

namespace TerribleSettingsAuditor.Abstractions.Attribute;

#nullable enable

[AttributeUsage(AttributeTargets.Property)]
public class BaggageItemAttribute : System.Attribute
{
    public string? Description { get; set; }

    public BaggageItemAttribute(string? description = null)
    {
        Description = description;
    }
}

#nullable disable
