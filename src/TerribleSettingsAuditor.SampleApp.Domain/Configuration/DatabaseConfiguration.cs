using System.ComponentModel.DataAnnotations;
using TerribleSettingsAuditor.Abstractions.Attribute;

namespace TerribleSettingsAuditor.SampleApp.Domain.Configuration;

[Luggage("Databases", "Database Connection strings")]
public class DatabaseConfiguration
{
    /// <summary>
    ///  Configuration Key. (i.e., Database:ConnectionStringSampleApp)
    /// </summary>
    public const string Position = "Database";
    
    [Required]
    [LuggageItem("SampleApp Connection String", true)]
    public string? ConnectionStringSampleApp { get; set; }
    
    [Required]
    [LuggageItem("UsersDb Connection String", true)]
    public string? ConnectionStringUsersDb { get; set; }
}
