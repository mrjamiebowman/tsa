using TerribleSettingsAuditor.Abstractions.Attribute;

namespace TerribleSettingsAuditor.SampleApp.Configuration;

[CarryOn("Databases", "Application settings", true)]
public class ApplicationOptions
{
    /// <summary>
    ///  Configuration Key. (i.e., Application:DebugMode)
    /// </summary>
    public const string Position = "Application";

    public bool DebugMode { get; set; } = false;

    [BaggageItem("Application Title", true)]
    public string? Title { get; set; }

    [BaggageItem("DoesntNeedToBeSet", false)]
    public bool? DoesntNeedToBeSet { get; set; }
}
