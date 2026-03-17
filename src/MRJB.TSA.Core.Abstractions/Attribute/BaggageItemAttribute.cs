using System;

namespace TerribleSettingsAuditor.Abstractions.Attribute;

#nullable enable

[AttributeUsage(AttributeTargets.Property)]
public class BaggageItemAttribute : System.Attribute
{
    public bool IsRequired { get; set; }

    public string? Description { get; set; }

    public BaggageItemAttribute(string? description = null, bool isRequired = false)
    {
        Description = description;
        IsRequired = isRequired;
    }
}

#nullable disable
