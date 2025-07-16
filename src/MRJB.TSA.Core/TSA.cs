using Microsoft.Extensions.Logging;
using MRJB.TSA.Abstractions.Attribute;
using MRJB.TSA.Core.Domain.Models;
using MRJB.TSA.Core.Models;
using System.Reflection;

namespace MRJB.TSA.Core;

public class TSA : ITSA
{
    // logger
    private ILogger<TSA> _logger;

    public TSA(ILogger<TSA> logger)
    {
        _logger = logger;
    }

    public Task<ScreeningReport> ValidateAsync(List<Assembly> assemblies, CancellationToken cancellationToken = default)
    {
        return ValidateAsync(assemblies, null, cancellationToken);
    }

    public async Task<ScreeningReport> ValidateAsync(List<Assembly> assemblies, Action<ScreeningSettings>? SsreeningSettingsAction = null, CancellationToken cancellationToken = default)
    {
        // result
        var screeningReport = new ScreeningReport();

        // default screening settings
        ScreeningSettings screeningSettings = new ScreeningSettings();

        if (SsreeningSettingsAction != null)
        {
            SsreeningSettingsAction.Invoke(screeningSettings);
        }

        await Task.Delay(50);

        // configurations
        List<ConfigurationEntry> configurations = new List<ConfigurationEntry>();

        foreach (var assembly in assemblies)
        {
            _logger.LogInformation($"Loaded Assembly: {assembly.FullName}");
            // Do something with the assembly

            configurations = await GetConfigurationsAsync(assemblies, cancellationToken);
        }

        // process configurations
        foreach (var config in configurations)
        {
            // validate

            // append
        }

        return screeningReport;
    }

    #region private methods

    public Task<List<ConfigurationEntry>> GetConfigurationsAsync(List<Assembly> assemblies, CancellationToken cancellationToken = default)
    {
        var configurationEntries = new List<ConfigurationEntry>();

        foreach (var assembly in assemblies)
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            var typesWithAttribute = assembly.GetTypes()
                .Where(t => t.IsClass && t.GetCustomAttribute<CarryOnAttribute>() != null);

            foreach (var type in typesWithAttribute)
            {
                // You can extract property info, values, metadata, etc., from these types
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                var configEntry = new ConfigurationEntry()
                {
                    ClassName = type.FullName!
                };

                foreach (var prop in properties)
                {
                    // You can adjust this to match your ConfigurationEntry needs

                    configEntry.Properties.Add(new ConfigurationProperty
                    {
                        PropertyName = prop.Name,
                        PropertyType = prop.PropertyType.FullName!
                    });
                }

                configurationEntries.Add(configEntry);
            }
        }

        return Task.FromResult(configurationEntries);
    }

    #endregion
}
