using System.ComponentModel.DataAnnotations;
using TerribleSettingsAuditor.Abstractions.Attribute;

namespace TerribleSettingsAuditor.SampleApp.Domain.Configuration;

[CarryOn("BadConfiguration", "Bad Configuration", true)]
public class BadConfiguration
{
    /// <summary>
    ///  Configuration Key. (i.e., Bad:DebugMode)
    /// </summary>
    public const string Position = "Bad";

    [BaggageItem("Bad Setting (Should fail)")]
    [Required]
    public bool? Baddie { get; set; }
}
