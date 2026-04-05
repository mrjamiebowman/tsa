using System;

namespace TerribleSettingsAuditor.Abstractions.Attribute;

#nullable enable

[AttributeUsage(AttributeTargets.Property)]
public class LuggageItemAttribute : System.Attribute
{
    public string? Description { get; set; }

    public bool Secret { get; set; }

    public LuggageItemAttribute(string? description = null)
    {
        Description = description;
    }

    public LuggageItemAttribute(string? description = null, bool secret = false)
    {
        Description = description;
        Secret = secret;
    }
}

#nullable disable
