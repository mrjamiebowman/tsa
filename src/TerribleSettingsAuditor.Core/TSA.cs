using Microsoft.Extensions.Logging;
using System.Reflection;
using TerribleSettingsAuditor.Abstractions.Attribute;
using TerribleSettingsAuditor.Core.Helpers;
using TerribleSettingsAuditor.Core.Interfaces;
using TerribleSettingsAuditor.Core.Models;

namespace TerribleSettingsAuditor.Core;

public class TSA : ITSA
{
    // logger
    private ILogger<TSA> _logger;

    public TSA(ILogger<TSA> logger)
    {
        _logger = logger;
    }

    public Task<ScreeningReport> ScreenAsync(IServiceProvider serviceProvider, Assembly[] assemblies, CancellationToken cancellationToken = default)
    {
        return ScreenAsync(serviceProvider, assemblies, null, cancellationToken);
    }

    public async Task<ScreeningReport> ScreenAsync(IServiceProvider serviceProvider, Assembly[] assemblies, Action<ScreeningSettings>? SsreeningSettingsAction = null, CancellationToken cancellationToken = default)
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

            var carryOn = new ConfigurationReport() {
                Name = configKey.ClassName,
                Namespace = configKey.Namespace
            };

            // resolve config class
            var config = ConfigResolver.ResolveConfig(serviceProvider, configKey.Type);

            // not found
            if (config == null) {
                throw new ArgumentNullException("Configuration class not found.");
            }

            var configType = config?.GetType();

            // carry-on
            var carryOnAttr = configType.GetCustomAttribute<CarryOnAttribute>();

            // properties
            var properties = configType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            /**************************************************/
            /*                   validation                   */
            /**************************************************/

            foreach (var prop in properties)
            {
                /**************************************************/
                /*          baggage item (property check)         */
                /**************************************************/

                var baggageAttr = prop.GetCustomAttribute<BaggageItemAttribute>();
                var baggageAttrConnectionString = prop.GetCustomAttribute<BaggageItemConnectionStringAttribute>();

                /**************************************************/
                /*                  validation                    */
                /**************************************************/

                // validate
                var result = PropertyValidator.ValidateProperty(config, prop.Name);

                bool pass = false;

                if (!result.Any()) {
                    pass = true;
                }

                // required
                var required = PropertyValidator.IsRequired(prop) ? true : false;

                // baggage item
                var baggageItem = new ConfigurationPropertyReport() {
                    BaggageItem = baggageAttr != null ? true : false,
                    Name = prop.Name,
                    Description = baggageAttr?.Description ?? String.Empty,
                    Pass = true,
                    Message = "",
                    Required = required
                };

                // baggage item
                carryOn.Properties.Add(baggageItem);
            }

            // configuration
            screeningReport.Configuration.Add(carryOn);
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
                    Namespace = type.Namespace,
                    ClassName = type.Name!,
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
