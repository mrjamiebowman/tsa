using MRJB.TSA.Core;
using MRJB.TSA.Core.Tests.Unit;
using System.Reflection;

namespace Validation;

public class ValidationTest : BaseTsaTest
{
    [Fact]
    public async Task Validate_Pass()
    {
        try
        {
            // arrange
            CancellationTokenSource cts = new();

            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(typeof(MRJB.TSA.SampleApp.Passport).Assembly);

            // logger
            var logger = GetLogger();

            // tsa
            var tsa = new TSA(logger);

            // act
            var result = await tsa.ValidateAsync(assemblies.ToArray(), cts.Token);

            // assert
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
