using Microsoft.Extensions.Logging;
using MRJB.TSA.Abstractions.Attribute;
using MRJB.TSA.Core.CLI;
using MRJB.TSA.Core.Interfaces;
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

    public Task<ScreeningReport> PreCheckAsync(IServiceProvider serviceProvider, Assembly[] assemblies, CancellationToken cancellationToken = default)
    {
        return PreCheckAsync(serviceProvider, assemblies, null, cancellationToken);
    }

    public async Task<ScreeningReport> PreCheckAsync(IServiceProvider serviceProvider, Assembly[] assemblies, Action<ScreeningSettings>? SsreeningSettingsAction = null, CancellationToken cancellationToken = default)
    {
        // result
        var screeningReport = new ScreeningReport();

        // default screening settings
        ScreeningSettings screeningSettings = new ScreeningSettings();

        if (SsreeningSettingsAction != null)
        {
            SsreeningSettingsAction.Invoke(screeningSettings);
        }

        // configurations
        List<ConfigurationEntry> configurations = new List<ConfigurationEntry>();

        // assemblies
        configurations = await GetConfigurationsAsync(serviceProvider, assemblies, cancellationToken);

        // process configurations
        foreach (var configKey in configurations)
        {
            /**************************************************/
            /*             carry-on (configuration)           */
            /**************************************************/

            TsaCli.WriteYellow($"Carry On: {configKey} (Configuration Class)");

            // validate
            var config = serviceProvider.GetService(configKey.Type);

            var configType = config.GetType();
            var properties = configType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // carry-on
            var carryOnAttr = configType.GetCustomAttribute<CarryOnAttribute>();

            foreach (var prop in properties)
            {
                // attributes
                var baggageAttr = prop.GetCustomAttribute<BaggageItemAttribute>();
                var baggageAttrConnectionString = prop.GetCustomAttribute<BaggageItemConnectionStringAttribute>();

                /**************************************************/
                /*          baggage item (property check)         */
                /**************************************************/

                if (carryOnAttr != null || baggageAttr != null)
                {
                    bool success = true;

                    var value = prop.GetValue(config);

                    var required = baggageAttr.IsRequired == true ? "Yes" : "No";

                    if (baggageAttr.IsRequired == true && String.IsNullOrWhiteSpace(value.ToString()))
                    {
                        success = false;
                    }

                    // get icon
                    string icon = "";

                    if (success == false)
                    {
                        icon = TsaCli.Icons.Failure;
                    } else
                    {
                        icon = TsaCli.Icons.Success;
                    }

                    TsaCli.WriteYellow($"{icon} Baggage Item: {prop.Name}, Required: {required}");
                }
            }
        }

        return screeningReport;
    }

    public Task<ScreeningReport> ValidateAsync(IServiceProvider serviceProvider, Assembly[] assemblies, CancellationToken cancellationToken = default)
    {
        return ValidateAsync(serviceProvider, assemblies, null, cancellationToken);
    }

    public async Task<ScreeningReport> ValidateAsync(IServiceProvider serviceProvider, Assembly[] assemblies, Action<ScreeningSettings>? SsreeningSettingsAction = null, CancellationToken cancellationToken = default)
    {
        // result
        var screeningReport = new ScreeningReport();

        // default screening settings
        ScreeningSettings screeningSettings = new ScreeningSettings();

        if (SsreeningSettingsAction != null)
        {
            SsreeningSettingsAction.Invoke(screeningSettings);
        }

        // configurations
        List<ConfigurationEntry> configurations = new List<ConfigurationEntry>();

        foreach (var assembly in assemblies)
        {
            //TsaCli.WriteGreen($"Loaded Assembly: {assembly.FullName}");
        }

        // assemblies
        configurations = await GetConfigurationsAsync(serviceProvider, assemblies, cancellationToken);

        // process configurations
        foreach (var config in configurations)
        {
            // validate

            // append
        }

        return screeningReport;
    }

    #region private methods

    public Task<List<ConfigurationEntry>> GetConfigurationsAsync(IServiceProvider serviceProvider, Assembly[] assemblies, CancellationToken cancellationToken = default)
    {
        var configurationEntries = new List<ConfigurationEntry>();

        foreach (var assembly in assemblies)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            var typesWithAttribute = assembly.GetTypes()
                                             .Where(t => t.IsClass && t.GetCustomAttribute<CarryOnAttribute>() != null);

            foreach (var type in typesWithAttribute)
            {
                // You can extract property info, values, metadata, etc., from these types
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                var configEntry = new ConfigurationEntry()
                { 
                    Assembly = assembly.FullName,
                    ClassName = type.FullName!,
                    Type = type
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
