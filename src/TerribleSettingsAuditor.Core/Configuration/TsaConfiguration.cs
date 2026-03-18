namespace TerribleSettingsAuditor.Core.Configuration;

public class TsaConfiguration
{
    public const string Position = "TSA";

    /// <summary>
    ///  This option verifies if the settings and configuration are present.
    /// </summary>
    public bool PreCheck { get; set; } = true;

    /// <summary>
    ///  Abort if Pre-Check Screening fails.
    /// </summary>
    public bool AbortPreCheckFailure { get; set; } = true;

    /// <summary>
    ///  This is a Pre-Check and Validation where TSA will not only inspect 
    ///  but test connection strings and settings to see if they are in fact valid.
    /// </summary>
    public bool Validate { get; set; } = false;

    /// <summary>
    ///  Abort validate failure.
    /// </summary>
    public bool AbortValidateFailure { get; set; } = true;

}
