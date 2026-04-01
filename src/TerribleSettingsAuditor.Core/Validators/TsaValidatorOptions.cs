namespace TerribleSettingsAuditor.Core.Validators;

public class TsaValidatorOptions
{
    public Dictionary<(Type, string), Action> _validators { get; } = new Dictionary<(Type, string), Action>();
}
