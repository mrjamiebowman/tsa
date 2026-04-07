using System.ComponentModel.DataAnnotations;
using TerribleSettingsAuditor.Abstractions.Attribute;

namespace TerribleSettingsAuditor.SampleApp.Domain.Configuration;

[Luggage("ApplicationOptions", "Application settings")]
public class ApplicationOptions
{
    /// <summary>
    ///  Configuration Key. (i.e., Application:DebugMode)
    /// </summary>
    public const string Position = "Application";

    public bool DebugMode { get; set; } = false;

    [Required]
    [LuggageItem("Application Title")]
    public string? Title { get; set; }

    [LuggageItem("DoesntNeedToBeSet")]
    public bool? DoesntNeedToBeSet { get; set; }
}
