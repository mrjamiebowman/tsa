using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using TerribleSettingsAuditor.Core.Interfaces;
using TerribleSettingsAuditor.Core.Validators;

namespace TerribleSettingsAuditor.Core;

public static class ConfigurationBuilderExtension
{
    public static OptionsBuilder<TOptions> ValidateWithTsa<TOptions>(this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
    {
        if (optionsBuilder == null) throw new ArgumentNullException(nameof(optionsBuilder));

        optionsBuilder.Services.TryAddTransient<ITsaConfigValidator, TsaConfigValidator>();

        optionsBuilder.Services.AddOptions<TsaValidatorOptions>()
            .Configure<IOptionsMonitor<TOptions>>((vo, options) =>
            {
                // This adds an action that resolves the options value to force evaluation
                // We don't care about the result as duplicates are not important
                vo._validators[(typeof(TOptions), optionsBuilder.Name)] = () => options.Get(optionsBuilder.Name);
            });

        return optionsBuilder;
    }
}
