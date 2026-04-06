using System;

namespace TerribleSettingsAuditor.Abstractions.Attribute;

#nullable enable

/// <summary>
///  Terrible Settings Auditor (TSA), Luggage
/// </summary>
/// <remarks>
///  Apply this to configuration classes to track it.
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class LuggageAttribute : System.Attribute
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public LuggageAttribute(string? description = null)
    {
        Name = name;
    }

    public LuggageAttribute(string? name = null, string? description = null)
    {
        Name = name;
        Description = description;
    }
}

#nullable disable
