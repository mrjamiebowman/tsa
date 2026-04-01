using System.ComponentModel.DataAnnotations;
using TerribleSettingsAuditor.Abstractions.Attribute;
using TerribleSettingsAuditor.Abstractions.Enums;

namespace TerribleSettingsAuditor.SampleApp.Domain.Configuration;

[Luggage("Databases", "Database Connection strings", true)]
public class DatabaseConfiguration
{
    /// <summary>
    ///  Configuration Key. (i.e., Database:ConnectionStringSampleApp)
    /// </summary>
    public const string Position = "Database";

    [LuggageItem("SampleApp Connection String")]
    [LuggageItemConnectionString(ConnectionStringEnum.SqlServer)]
    [Required]
    public string? ConnectionStringSampleApp { get; set; }

    [LuggageItem("UsersDb Connection String")]
    [LuggageItemConnectionString(ConnectionStringEnum.SqlServer)]
    [Required]
    public string? ConnectionStringUsersDb { get; set; }
}
