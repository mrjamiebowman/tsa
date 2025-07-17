using MRJB.TSA.Core.Models;
using System.Reflection;

namespace MRJB.TSA.Core.Interfaces;

public interface ITSA
{
    Task<List<ConfigurationEntry>> GetConfigurationsAsync(List<Assembly> assemblies, CancellationToken cancellationToken = default);
}
