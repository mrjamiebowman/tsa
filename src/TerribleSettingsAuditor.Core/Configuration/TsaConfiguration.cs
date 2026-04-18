namespace TerribleSettingsAuditor.Core.Configuration;

public class TsaConfiguration
{
    public const string Position = "TSA";

    /// <summary>
    ///  This option screens configuration on Startup.
    /// </summary>
    public bool ScreenOnStartup { get; set; } = true;

    /// <summary>
    ///  Abort if Screening fails on Startup
    /// </summary>
    public bool AbortScreenFailure { get; set; }

    /// <summary>
    ///  This limits how many stars are used when exposing a secret.
    ///  i.e, "asdfdf***********************************************************2143"
    /// </summary>
    public int? DefaultMaxExposeSecretLength { get; set; } = 30;
}
