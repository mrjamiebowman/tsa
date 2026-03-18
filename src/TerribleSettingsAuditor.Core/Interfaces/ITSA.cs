using System.Reflection;
using TerribleSettingsAuditor.Core.Models;

namespace TerribleSettingsAuditor.Core.Interfaces;

public interface ITSA
{
    Task<ScreeningReport> PreCheckAsync(IServiceProvider serviceProvider, Assembly[] assemblies, CancellationToken cancellationToken = default);

    Task<ScreeningReport> PreCheckAsync(IServiceProvider serviceProvider, Assembly[] assemblies, Action<ScreeningSettings>? SsreeningSettingsAction = null, CancellationToken cancellationToken = default);

    Task<List<ConfigurationEntry>> GetConfigurationsAsync(IServiceProvider serviceProvider, Assembly[] assemblies, CancellationToken cancellationToken = default);

    Task<ScreeningReport> ValidateAsync(IServiceProvider serviceProvider, Assembly[] assemblies, Action<ScreeningSettings>? SsreeningSettingsAction = null, CancellationToken cancellationToken = default);
}
