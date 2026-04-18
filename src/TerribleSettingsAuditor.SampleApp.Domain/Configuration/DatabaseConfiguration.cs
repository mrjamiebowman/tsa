using System.ComponentModel.DataAnnotations;
using TerribleSettingsAuditor.Abstractions.Attribute;

namespace TerribleSettingsAuditor.SampleApp.Domain.Configuration;

[Luggage("Database Connection strings", Pinned = true)]
public class DatabaseConfiguration
{
    /// <summary>
    ///  Configuration Key. (i.e., Database:ConnectionStringSampleApp)
    /// </summary>
    public const string Position = "Database";
    
    [Required]
    [LuggageItem("SampleApp Connection String")]
    public string? ConnectionStringSampleApp { get; set; }
    
    [Required]
    [LuggageItem("UsersDb Connection String")]
    public string? ConnectionStringUsersDb { get; set; }
}
