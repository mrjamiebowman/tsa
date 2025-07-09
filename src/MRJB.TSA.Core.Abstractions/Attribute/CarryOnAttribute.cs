using System;

namespace MRJB.TSA.Abstractions.Attribute;

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

    public CarryOnAttribute(bool isRequired = false, string? name = null, string? description = null)
    {
        IsRequired = isRequired;
        Name = name;
        Description = description;
    }
}
