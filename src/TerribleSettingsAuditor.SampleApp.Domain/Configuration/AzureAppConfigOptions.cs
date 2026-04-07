using System.ComponentModel.DataAnnotations;
using TerribleSettingsAuditor.Abstractions.Attribute;

namespace TerribleSettingsAuditor.SampleApp.Domain.Configuration;

[Luggage("Azure App Config", Order = 1, Pinned = true)]
public class AzureAppConfigOptions
{
    public const string Position = "AzureAppConfig";

    public bool Enabled { get; set; }

    [Required]
    [LuggageItem("Azure App Config Connection String", Secret = true, Expose = true, ShowLeft = 10, ShowRight = 0)]
    public string? ConnectionString { get; set; }

    [Required]
    [LuggageItem("Client ID", Expose = true)]
    public string? ClientId { get; set; }

    [Required]
    [LuggageItem("Client Secret", Secret = true, Expose = true)]
    public string? ClientSecret { get; set; }

    [Required]
    [LuggageItem("Tenant ID", Secret = false, Expose = true)]
    public string? TenantId { get; set; }
}
