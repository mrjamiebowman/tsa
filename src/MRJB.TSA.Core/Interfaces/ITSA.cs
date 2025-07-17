using MRJB.TSA.Core.Models;
using System.Reflection;

namespace MRJB.TSA.Core.Interfaces;

public interface ITSA
{
    Task<ScreeningReport> PreCheckAsync(Assembly[] assemblies, CancellationToken cancellationToken = default);

    Task<ScreeningReport> PreCheckAsync(Assembly[] assemblies, Action<ScreeningSettings>? SsreeningSettingsAction = null, CancellationToken cancellationToken = default);

    Task<List<ConfigurationEntry>> GetConfigurationsAsync(Assembly[] assemblies, CancellationToken cancellationToken = default);

    Task<ScreeningReport> ValidateAsync(Assembly[] assemblies, Action<ScreeningSettings>? SsreeningSettingsAction = null, CancellationToken cancellationToken = default);
}
