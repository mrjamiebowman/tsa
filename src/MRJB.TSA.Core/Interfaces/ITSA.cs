using MRJB.TSA.Core.Models;
using System.Reflection;

namespace MRJB.TSA.Core.Interfaces;

public interface ITSA
{
    Task<ScreeningReport> PreCheckAsync(IServiceProvider serviceProvider, Assembly[] assemblies, CancellationToken cancellationToken = default);

    Task<ScreeningReport> PreCheckAsync(IServiceProvider serviceProvider, Assembly[] assemblies, Action<ScreeningSettings>? SsreeningSettingsAction = null, CancellationToken cancellationToken = default);

    Task<List<ConfigurationEntry>> GetConfigurationsAsync(IServiceProvider serviceProvider, Assembly[] assemblies, CancellationToken cancellationToken = default);

    Task<ScreeningReport> ValidateAsync(IServiceProvider serviceProvider, Assembly[] assemblies, Action<ScreeningSettings>? SsreeningSettingsAction = null, CancellationToken cancellationToken = default);
}
