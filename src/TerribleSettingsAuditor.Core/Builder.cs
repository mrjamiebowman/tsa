using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TerribleSettingsAuditor.Core.CLI;
using TerribleSettingsAuditor.Core.Configuration;
using TerribleSettingsAuditor.Core.Interfaces;
using TerribleSettingsAuditor.Core.Validators;

namespace TerribleSettingsAuditor.Core;

public static class Builder
{
    private static TsaConfiguration? TsaConfiguration { get; set; }

    /// <summary>
    ///  Add Terrible Settings Auditor (TSA)
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static TBuilder AddTerribleSettingsAuditor<TBuilder>(this TBuilder builder, Action<TsaConfiguration>? tsaSettings = default) where TBuilder : IHostApplicationBuilder
    {
        // configuration
        TsaConfiguration tsaConfiguration = new TsaConfiguration();
        builder.Configuration.GetSection(TsaConfiguration.Position).Bind(tsaConfiguration);
        TsaConfiguration = tsaConfiguration;

        if (tsaSettings != null)
        {
            tsaSettings.Invoke(tsaConfiguration);
        }

        builder.Services.AddSingleton(tsaConfiguration);

        // services
        builder.Services.AddTransient<ITSA, TSA>();

        return builder;
    }

    /// <summary>
    ///  Use Terrible Settings Auditor (TSA)
    /// </summary>
    /// <param name="app"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task<IApplicationBuilder> UseTerribleSettingsAuditorAsync(this IApplicationBuilder app, string[]? args = null)
    {
        //var validator = app.ApplicationServices.GetRequiredService<ITsaConfigValidator>();
        //validator.Validate();

        // tsa configuration
        TsaConfiguration tsaConfiguration = app.ApplicationServices.GetRequiredService<TsaConfiguration>();

        // cts
        CancellationTokenSource cts = new CancellationTokenSource();

        // unicode
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // cli
        await ProcessCliCommandsAsync(app, args, cts.Token);

        // process config
        await ProcessAsync(app, args, cts.Token);

        return app;
    }

    private static async Task ProcessAsync(IApplicationBuilder app, string[]? args = null, CancellationToken cancellationToken = default)
    {
        // configuration
        TsaConfiguration tsaConfiguration = app.ApplicationServices.GetRequiredService<TsaConfiguration>();

        if (tsaConfiguration.ScreenOnStartup == false)
        {
            return;
        }

        // banner
        TsaCli.ShowBanner();

        // tsa
        var tsa = app.ApplicationServices.GetRequiredService<ITSA>();

        // tsa: screen
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        // screening report
        var screeningReport = await tsa.ScreenAsync(app.ApplicationServices, assemblies, cancellationToken);

        // render report
        TsaCli.ShowReport(screeningReport);
        
        // cli: ci/cd
        if (screeningReport.Pass == false && tsaConfiguration.AbortScreenFailure == true)
        {
            // fail
            Environment.Exit(1);
        }
    }

    private static async Task ProcessCliCommandsAsync(IApplicationBuilder app, string[]? args = null, CancellationToken cancellationToken = default)
    {
        if (
            (args == null || args.Length == 0 || args?.Contains("tsa") == false))
        {
            // if we aren't validating on startup and we don't have a tsa argument then return...
            return;
        }

        // tsa
        var tsa = app.ApplicationServices.GetRequiredService<ITSA>();

        // help
        if (args[0] == "tsa" && (args[1] == "--help" || args[1] == "-h"))
        {
            TsaCli.ShowHelp();
            Environment.Exit(0);
        }

        // tsa: generate config
        if (args[0] == "tsa" && (args[1] == "--generate-config" || args[1] == "-gc"))
        {
            TsaCli.WriteYellow("Generating TSA Config...");
            Environment.Exit(0);
        }

        // tsa: joke
        if (args[0] == "tsa" && (args[1] == "--joke" || args[1] == "-j"))
        {
            TsaCli.GenerateJoke();
            Environment.Exit(0);
        }

        // tsa: screen
        if (args[0] == "tsa" && (args[1] == "--screen" || args[1] == "-s"))
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // abort
            bool cliNoAbort = args.Contains("--no-abort");

            // screening report
            var screeningReport = await tsa.ScreenAsync(app.ApplicationServices, assemblies, cancellationToken);

            // render report
            TsaCli.ShowReport(screeningReport);

            if (cliNoAbort)
            {
                // return app or this will stop execution.
                return;
            }
            else
            {
                // cli: ci/cd
                if (screeningReport.Pass == true)
                {
                    // success
                    Environment.Exit(0);
                }
                else
                {
                    // fail
                    Environment.Exit(1);
                }
            }
        }

        //// tsa: validate
        //if (args[0] == "tsa" && (args[1] == "--validate" || args[1] == "-v"))
        //{
        //    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        //    var screeningReport = await tsa.ValidateAsync(app.ApplicationServices, assemblies);
        //    TsaCli.ShowReport(screeningReport);

        //    if (tsaConfiguration.AbortValidationFailure == false || screeningReport.Pass == true)
        //    {
        //        Environment.Exit(0);
        //    }
        //    else
        //    {
        //        Environment.Exit(1);
        //    }
        //}
    }

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
