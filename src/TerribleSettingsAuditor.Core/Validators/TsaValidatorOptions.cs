namespace TerribleSettingsAuditor.Core.Validators;

public class TsaValidatorOptions
{
    public Dictionary<(Type OptionsType, string Name), TsaValidatorRegistration> _validators { get; } = new();
}
