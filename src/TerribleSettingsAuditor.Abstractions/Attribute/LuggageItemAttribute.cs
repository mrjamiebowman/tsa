using System;

namespace TerribleSettingsAuditor.Abstractions.Attribute;

#nullable enable

[AttributeUsage(AttributeTargets.Property)]
public class LuggageItemAttribute : System.Attribute
{
    public string? Description { get; set; }

    /// <summary>
    ///  Is this a secret? Connection Strings? API Keys?
    /// </summary>
    public bool Secret { get; set; }

    /// <summary>
    ///  If this is set to true, it will expose the value on the report.
    ///  If the value happens to be a secret it will be masked and truncated while
    ///  showing the first and last 4 characters. This is based on a scale.
    /// </summary>
    public bool Expose { get; set; }

    /// <summary>
    /// Number of characters to show from the left when masking.
    /// </summary>
    public int ShowLeft { get; set; } = 0;

    /// <summary>
    /// Number of characters to show from the right when masking.
    /// </summary>
    public int ShowRight { get; set; } = 0;

    public LuggageItemAttribute(string? description = null)
    {
        Description = description;
    }
}

#nullable disable
