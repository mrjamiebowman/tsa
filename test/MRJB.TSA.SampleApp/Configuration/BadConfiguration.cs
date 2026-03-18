using TerribleSettingsAuditor.Abstractions.Attribute;

namespace TerribleSettingsAuditor.SampleApp.Configuration;

public class BadConfiguration
{
    /// <summary>
    ///  Configuration Key. (i.e., Bad:DebugMode)
    /// </summary>
    public const string Position = "Bad";

    [BaggageItem("Bad Setting (Should fail)", true)]
    public bool? Baddie { get; set; }
}
