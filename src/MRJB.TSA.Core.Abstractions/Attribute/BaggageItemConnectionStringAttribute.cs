using MRJB.TSA.Abstractions.Enums;
using System;

namespace MRJB.TSA.Abstractions.Attribute;

[AttributeUsage(AttributeTargets.Property)]
public class BaggageItemConnectionStringAttribute : System.Attribute
{
    public string? Name { get; set; }

    public ConnectionStringEnum? ConnectionStringType { get; set; }

    public BaggageItemConnectionStringAttribute(string? name = null, ConnectionStringEnum? connectionStringType = ConnectionStringEnum.SqlServer)
    {
        Name = name;
        ConnectionStringType = connectionStringType;
    }
}
