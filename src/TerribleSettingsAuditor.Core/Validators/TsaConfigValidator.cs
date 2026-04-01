using Microsoft.Extensions.Options;
using System.Runtime.ExceptionServices;
using TerribleSettingsAuditor.Core.Interfaces;

namespace TerribleSettingsAuditor.Core.Validators;

public class TsaConfigValidator : ITsaConfigValidator
{
    private readonly TsaValidatorOptions _validatorOptions;

    public TsaConfigValidator(IOptions<TsaValidatorOptions> validators)
    {
        _validatorOptions = validators.Value;
    }

    public void Validate()
    {
        List<Exception>? exceptions = null;

        foreach (Action validator in _validatorOptions._validators.Values)
        {
            try
            {
                // Execute the validation method and catch the validation error
                validator();
            }
            catch (OptionsValidationException ex)
            {
                exceptions ??= new();
                exceptions.Add(ex);
            }
        }

        if (exceptions != null)
        {
            if (exceptions.Count == 1)
            {
                // Rethrow if it's a single error
                ExceptionDispatchInfo.Capture(exceptions[0]).Throw();
            }

            if (exceptions.Count > 1)
            {
                // Aggregate if we have many errors
                throw new AggregateException(exceptions);
            }
        }
    }
}
