using System;

namespace MRJB.TSA.Abstractions.Attribute;

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
