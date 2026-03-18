using MRJB.TSA.Core.Domain.Models;
using System.Reflection;

namespace MRJB.TSA.Core;

public interface ITSA
{
    Task<List<ConfigurationEntry>> GetConfigurationsAsync(List<Assembly> assemblies, CancellationToken cancellationToken = default);
}
