using System;
using TerribleSettingsAuditor.Abstractions.Enums;

namespace TerribleSettingsAuditor.Abstractions.Attribute;

[AttributeUsage(AttributeTargets.Property)]
public class BaggageItemConnectionStringAttribute : System.Attribute
{
    public ConnectionStringEnum ConnectionStringType { get; set; }

    public BaggageItemConnectionStringAttribute(ConnectionStringEnum connectionStringType = ConnectionStringEnum.SqlServer)
    {
        ConnectionStringType = connectionStringType;
    }
}
