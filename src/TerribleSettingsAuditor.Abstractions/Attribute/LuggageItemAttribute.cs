using System;

namespace TerribleSettingsAuditor.Abstractions.Attribute;

#nullable enable

[AttributeUsage(AttributeTargets.Property)]
public class LuggageItemAttribute : System.Attribute
{
    public string? Description { get; set; }

    public LuggageItemAttribute(string? description = null)
    {
        Description = description;
    }
}

#nullable disable
