using System.ComponentModel.DataAnnotations;
using TerribleSettingsAuditor.Abstractions.Attribute;

namespace TerribleSettingsAuditor.SampleApp.Domain.Configuration;

[Luggage("Application Settings")]
public class ApplicationOptions
{
    /// <summary>
    ///  Configuration Key. (i.e., Application:DebugMode)
    /// </summary>
    public const string Position = "Application";

    public bool DebugMode { get; set; } = false;

    [LuggageItem("Application Title")]
    [Required]
    public string? Title { get; set; }

    [LuggageItem("DoesntNeedToBeSet")]
    public bool? DoesntNeedToBeSet { get; set; }
}
