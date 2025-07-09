using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection;

namespace MRJB.TSA.Core.Tests.Unit;

public class ValidationTest : BaseTsaTest
{
    [Fact]
    public async Task Validate_Pass()
    {
        try
        {
            // arrange
            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(typeof(Program).Assembly);

            // logger
            var logger = base.GetLogger();

            // tsa
            var tsa = new TSA(logger);

            // act
            var result = await tsa.ValidateAsync(assemblies);

            // assert
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
