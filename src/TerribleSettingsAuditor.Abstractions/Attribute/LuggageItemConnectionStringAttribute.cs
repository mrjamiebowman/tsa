using System;
using TerribleSettingsAuditor.Abstractions.Enums;

namespace TerribleSettingsAuditor.Abstractions.Attribute;

[AttributeUsage(AttributeTargets.Property)]
public class LuggageItemConnectionStringAttribute : System.Attribute
{
    public ConnectionStringEnum ConnectionStringType { get; set; }

    public LuggageItemConnectionStringAttribute(ConnectionStringEnum connectionStringType = ConnectionStringEnum.SqlServer)
    {
        ConnectionStringType = connectionStringType;
    }
}
