using System.ComponentModel.DataAnnotations;

namespace TerribleSettingsAuditor.SampleApp.Domain.Configuration;

public class LibraryConfiguration
{
    public const string Position = "Library";

    [Required]
    public string? Name { get; set; }
}
