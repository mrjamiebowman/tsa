# Terrible Settings Auditor (TSA)
Terrible Settings Auditor is an independent developer tool and is not affiliated with or endorsed by the Transportation Security Administration.   
This tool is used for auditing and generating configuration in CI/CD pipelines or on demand configuration testing.   

## Attributes
We combine attributes with DataAnnotations to validate configuration. 

### [Luggage]

```csharp
[Luggage("ApplicationOptions", "Application settings")]
public class ApplicationOptions
{
    /// <summary>
    ///  Configuration Key. (i.e., Application:DebugMode)
    /// </summary>
    public const string Position = "Application";

    public bool DebugMode { get; set; } = false;

    [Required]
    public string? Title { get; set; }

    public bool? DoesntNeedToBeSet { get; set; }
}
```


