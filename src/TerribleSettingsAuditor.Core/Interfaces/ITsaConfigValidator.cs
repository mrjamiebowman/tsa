namespace TerribleSettingsAuditor.Core.Interfaces;

public interface ITsaConfigValidator
{
    /// <summary>
    /// Calls the <see cref="IValidateOptions{TOptions}"/> validators.
    /// </summary>
    /// <exception cref="OptionsValidationException">One or more <see cref="IValidateOptions{TOptions}"/> return failed <see cref="ValidateOptionsResult"/> when validating.</exception>
    void Validate();
}
