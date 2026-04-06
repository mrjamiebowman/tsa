using System.ComponentModel.DataAnnotations;
using TerribleSettingsAuditor.Abstractions.Attribute;

namespace TerribleSettingsAuditor.SampleApp.Domain.Configuration;

[Luggage("BadConfiguration", "Bad Configuration")]
public class BadConfiguration
{
    /// <summary>
    ///  Configuration Key. (i.e., Bad:DebugMode)
    /// </summary>
    public const string Position = "Bad";

    [LuggageItem("Bad Setting (Should fail)")]
    [Required]
    public bool? Baddie { get; set; }
}
