using TerribleSettingsAuditor.Abstractions.Attribute;
using TerribleSettingsAuditor.SampleApp.Domain.Configuration;

namespace TerribleSettingsAuditor.SampleApp.Domain;

[BaggageCart("appsettings")]
public class ConfigurationSchema
{
    public ApplicationOptions? ApplicationOptions { get; set; }

    public BadConfiguration? BadConfiguration { get; set; }

    public DatabaseConfiguration? DatabaseConfiguration { get; set; }

    public LibraryConfiguration? LibraryConfiguration { get; set; }
}
