using System.ComponentModel.DataAnnotations;
using TerribleSettingsAuditor.Abstractions.Attribute;
using TerribleSettingsAuditor.Abstractions.Enums;

namespace TerribleSettingsAuditor.SampleApp.Configuration;

[CarryOn("Databases", "Database Connection strings", true)]
public class DatabaseConfiguration
{
    /// <summary>
    ///  Configuration Key. (i.e., Database:ConnectionStringSampleApp)
    /// </summary>
    public const string Position = "Database";

    [BaggageItem("SampleApp Connection String")]
    [BaggageItemConnectionString(ConnectionStringEnum.SqlServer)]
    [Required]
    public string? ConnectionStringSampleApp { get; set; }

    [BaggageItem("UsersDb Connection String")]
    [BaggageItemConnectionString(ConnectionStringEnum.SqlServer)]
    [Required]
    public string? ConnectionStringUsersDb { get; set; }
}
