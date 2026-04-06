# ✈️  Terrible Settings Auditor (TSA)
"We screen your app settings before they crash on takeoff."

An open-source modern .NET tool that audits, validates your application's configuration and environment — all with TSA-grade scrutiny.

* TSA Screen - Fast screen to ensure essential config values are present (like connection strings, secrets, values, etc).

---

📦 NuGet Packages
TSA.Abstractions: Low-level package for attributes and configuration. (.NET Standard 2.0 for max compatibility)

TSA.Core: Shared logic for config analysis.

TSA.CLI: Command-line interface used in CI/CD pipelines.

--- 

### ✈️ TSA Vocabulary   
We used creative names to distinguish our attributes.

* Carry-On – Configuration class the app can carry along.

* Baggage-Item - Individual configuration property.

#### Sample Configuration
TSA tracks your bags by using attributes. `[CarryOn]` applies to the classs and `[BaggageItem]` applies to the properties.

```csharp
[CarryOn("Databases", "Application settings", true)]
public class ApplicationOptions
{
    /// <summary>
    ///  Configuration Key. (i.e., Application:DebugMode)
    /// </summary>
    public const string Position = "Application";

    public bool DebugMode { get; set; } = false;

    [BaggageItem("Application Title", true)]
    public string? Title { get; set; }

    [BaggageItem("DoesntNeedToBeSet", false)]
    public bool? DoesntNeedToBeSet { get; set; }
}
```

```bash
> SampleApp.exe tsa --screen
✅ SQLConnectionString: Present   
⚠️ RedisCacheKey: Missing   
✅ ApiBaseUrl: Present   
```

