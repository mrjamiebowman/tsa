using MRJB.TSA.Abstractions.Enums;
using System;

namespace MRJB.TSA.Abstractions.Attribute;

[AttributeUsage(AttributeTargets.Property)]
public class BaggageItemAttribute : System.Attribute
{
    public bool IsRequired { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public BaggageItemAttribute(bool isRequired = false, string? name = null, string? description = null)
    {
        IsRequired = isRequired;
        Name = name;
        Description = description;
    }
}
