# ✈️  Terrible Settings Auditor (TSA)
"We screen your app settings before they crash on takeoff."

An open-source modern .NET tool that audits, validates your application's configuration and environment — all with TSA-grade scrutiny.

* TSA PreCheck - Fast scan to ensure essential config values are present (like connection strings, secrets, values, etc).

---

📦 NuGet Packages
TSA.Abstractions: Low-level package for attributes and configuration. (.NET Standard 2.0 for max compatibility)

TSA.Core: Shared logic for config analysis.

TSA.CLI: Command-line interface used in CI/CD pipelines.

--- 

✈️ TSA Vocabulary
No-Fly List – Settings that are banned (e.g., plaintext passwords, SA user, AllowAnyOrigin in CORS).

Carry-On – Configuration class the app can carry along.

Baggage-Item - Individual configuration property.

```
> SampleApp.exe tsa --precheck   
✅ SQLConnectionString: Present   
⚠️ RedisCacheKey: Missing   
✅ ApiBaseUrl: Present   
```

