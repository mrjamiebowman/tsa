using Microsoft.Extensions.Logging;
using MRJB.TSA.Core.Domain.Models;
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

        foreach (var assembly in assemblies)
        {
            Console.WriteLine($"Loaded Assembly: {assembly.FullName}");
            // Do something with the assembly
        }

        return screeningReport;
    }
}
