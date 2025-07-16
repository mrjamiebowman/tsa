using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MRJB.TSA.Core;
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

        // tsa
        var tsa = app.ApplicationServices.GetRequiredService<ITSA>();

        // args
        if (args?.Contains("--validate") == true)
        {
            Console.WriteLine("Exiting due to --fail-fast argument.");
            Environment.Exit(1);
        }

        return app;
    }

}
