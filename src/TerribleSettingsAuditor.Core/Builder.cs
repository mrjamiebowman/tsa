using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TerribleSettingsAuditor.Core;
using TerribleSettingsAuditor.Core.CLI;
using TerribleSettingsAuditor.Core.Configuration;
using TerribleSettingsAuditor.Core.Interfaces;

namespace Microsoft.Extensions.Hosting;

public static class Builder
{
    /// <summary>
    ///  Add Terrible Settings Auditor (TSA)
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddTerribleSettingsAuditor(this IServiceCollection services, IConfiguration configuration, Action<TsaConfiguration>? tsaSettings = default)
    {
        // configuration
        TsaConfiguration tsaConfiguration = new TsaConfiguration();
        configuration.GetSection(TsaConfiguration.Position).Bind(tsaConfiguration);

        if (tsaSettings != null)
        {
            tsaSettings.Invoke(tsaConfiguration);
        }

        services.AddSingleton(tsaConfiguration);

        // services
        services.AddTransient<ITSA, TSA>();

        return services;
    }

    /// <summary>
    ///  Use Terrible Settings Auditor (TSA)
    /// </summary>
    /// <param name="app"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task<IApplicationBuilder> UseTerribleSettingsAuditorAsync(this IApplicationBuilder app, string[]? args = null)
    {
        if (args == null || args.Length == 0 || args?.Contains("tsa") == false)
        {
            return app;
        }

        // cts
        CancellationTokenSource cts = new CancellationTokenSource();

        // unicode
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // banner
        TsaCli.ShowBanner();

        // help
        if (args[0] == "tsa" && (args[1] == "--help" || args[1] == "-h"))
        {
            TsaCli.ShowHelp();
            Environment.Exit(1);
        }

        // tsa
        var tsa = app.ApplicationServices.GetRequiredService<ITSA>();

        // tsa: generate config
        if (args[0] == "tsa" && (args[1] == "--generate-config" || args[1] == "-gc"))
        {
            TsaCli.WriteYellow("Generating TSA Config...");
            Environment.Exit(1);
        }

        // tsa: joke
        if (args[0] == "tsa" && (args[1] == "--joke" || args[1] == "-j"))
        {
            TsaCli.GenerateJoke();
            Environment.Exit(1);
        }

        // tsa: scan
        if (args[0] == "tsa" && (args[1] == "--screen" || args[1] == "-s"))
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // screening report
            var screeningReport = await tsa.ScreenAsync(app.ApplicationServices, assemblies, cts.Token);

            // render report
            TsaCli.ShowReport(screeningReport);

            Environment.Exit(1);
        }

        // tsa: validate
        if (args[0] == "tsa" && (args[1] == "--validate" || args[1] == "-v"))
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var screeningReport = await tsa.ValidateAsync(app.ApplicationServices, assemblies);
            TsaCli.ShowReport(screeningReport);
            Environment.Exit(1);
        }

        return app;
    }
}
