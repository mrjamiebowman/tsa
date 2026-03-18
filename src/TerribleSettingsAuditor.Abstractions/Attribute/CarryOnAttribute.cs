using System;

namespace TerribleSettingsAuditor.Abstractions.Attribute;

#nullable enable

/// <summary>
///  Terrible Settings Auditor (TSA), CarryOn
/// </summary>
/// <remarks>
///  Apply this to configuration classes to track it.
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class CarryOnAttribute : System.Attribute
{
    public bool IsRequired { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public CarryOnAttribute(string? name = null)
    {
        Name = name;
    }

    public CarryOnAttribute(string? name = null, string? description = null, bool isRequired = false)
    {
        IsRequired = isRequired;
        Name = name;
        Description = description;
    }
}

#nullable disable
