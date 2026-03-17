using MRJB.TSA.Abstractions.Enums;
using System;

namespace MRJB.TSA.Abstractions.Attribute;

[AttributeUsage(AttributeTargets.Property)]
public class BaggageItemConnectionStringAttribute : System.Attribute
{
    public ConnectionStringEnum ConnectionStringType { get; set; }

    public BaggageItemConnectionStringAttribute(ConnectionStringEnum connectionStringType = ConnectionStringEnum.SqlServer)
    {
        ConnectionStringType = connectionStringType;
    }
}
