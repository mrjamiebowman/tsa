using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MRJB.TSA.Core;
using MRJB.TSA.Core.CLI;
using MRJB.TSA.Core.Settings;

namespace Microsoft.Extensions.DependencyInjection;

public static class Builder
{
    /// <summary>
    ///  Add Terrible Settings Auditor (TSA)
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddTerribleSettingsAuditor(this IServiceCollection services, IConfiguration configuration, Action<TsaSettings>? tsaSettings = default)
    {
        // configuration

        // services
        services.AddTransient<ITSA, TSA>();

        return services;
    }

    public static IApplicationBuilder UseTerribleSettingsAuditor(this IApplicationBuilder app, string[]? args = null)
    {
        ArgumentNullException.ThrowIfNull(app);

        if (args == null || args.Length == 0  || args?.Contains("tsa") == false)
        {
            return app;
        }

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

        // tsa: validate
        if (args[0] == "tsa" && (args[1] == "--joke" || args[1] == "-j"))
        {
            TsaCli.GenerateJoke();
            Environment.Exit(1);
        }

        // tsa: validate
        if (args[0] == "tsa" && (args[1] == "--validate" || args[1] == "-v"))
        {
            TsaCli.ShowReport();
            Environment.Exit(1);
        }

        return app;
    }   
}
