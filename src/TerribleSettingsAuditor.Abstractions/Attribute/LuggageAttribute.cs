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
    public string? Description { get; set; }

    /// <summary>
    ///  Pinned to Top. This puts the Configuration at the top of the report and makes it more visible.
    ///  This is useful for important configurations that you want to make sure are seen by the user.
    /// </summary>
    public bool Pinned { get; set; }

    public int? Order { get; set; }

    public LuggageAttribute()
    {

    }

    public LuggageAttribute(string description)
    {
        Description = description;
    }
}

#nullable disable
