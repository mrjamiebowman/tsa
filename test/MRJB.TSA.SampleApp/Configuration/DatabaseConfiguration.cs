using TerribleSettingsAuditor.Abstractions.Attribute;
using TerribleSettingsAuditor.Abstractions.Enums;

namespace TerribleSettingsAuditor.SampleApp.Configuration;

[CarryOn("Databases", "Database Connection strings", true)]
public class DatabaseConfiguration
{
    public const string Position = "Database";

    [BaggageItem("SampleApp Connection String", true)]
    [BaggageItemConnectionString(ConnectionStringEnum.SqlServer)]
    public string? ConnectionStringSampleApp { get; set; }

    [BaggageItem("UsersDb Connection String", true)]
    [BaggageItemConnectionString(ConnectionStringEnum.SqlServer)]
    public string? ConnectionStringUsersDb { get; set; }
}
