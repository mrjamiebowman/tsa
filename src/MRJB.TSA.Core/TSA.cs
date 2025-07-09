using Microsoft.Extensions.Logging;
using MRJB.TSA.Abstractions.Attribute;
using MRJB.TSA.Core.Domain.Models;
using System.Collections.Generic;
using System.Reflection;

namespace MRJB.TSA.Core;

public class TSA : ITSA
{
    private ILogger<TSA> _logger;

    public TSA(ILogger<TSA> logger)
    {
        _logger = logger;
    }

    public async Task<ScreeningReport> ValidateAsync(List<Assembly> assemblies, CancellationToken cancellationToken = default)
    {
        // result
        var screeningReport = new ScreeningReport();

        await Task.Delay(10);

        // configurations
        List<ConfigurationEntry> configurations = new List<ConfigurationEntry>();

        foreach (var assembly in assemblies)
        {
            _logger.LogInformation($"Loaded Assembly: {assembly.FullName}");
            // Do something with the assembly

            configurations = await GetConfigurationsAsync(assemblies, cancellationToken);
        }

        return screeningReport;
    }

    #region private methods

    public async Task<List<ConfigurationEntry>> GetConfigurationsAsync(List<Assembly> assemblies, CancellationToken cancellationToken = default)
    {
        await Task.Delay(10);

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

        return configurationEntries;
    }

    #endregion
}
