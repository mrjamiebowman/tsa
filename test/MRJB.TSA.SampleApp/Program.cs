using Scalar.AspNetCore;
using TerribleSettingsAuditor.SampleApp.Domain.Configuration;
using TerribleSettingsAuditor.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

/****************************************/
/*            configuration             */
/****************************************/

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();

// this is one way to validate configuration but it only validates on run...
// if we want to see this in a CI/CD pipeline then TSA is the way to go!
//builder.Services
//    .AddOptions<ApplicationOptions>()
//    .Bind(builder.Configuration.GetSection(ApplicationOptions.Position))
//    .ValidateDataAnnotations()
//    .ValidateOnStart()
//;

//builder.Services
//    .AddOptions<BadConfiguration>()
//    .Bind(builder.Configuration.GetSection(BadConfiguration.Position))
//    .ValidateDataAnnotations()
//    .ValidateOnStart();

/****************************************/
/*     tsa (not using attributes)       */
/****************************************/

builder.Services
    .AddOptions<ApplicationOptions>()
    .Bind(builder.Configuration.GetSection(ApplicationOptions.Position))
    .ValidateDataAnnotations()
    .ValidateWithTsa();

builder.Services
    .AddOptions<BadConfiguration>()
    .Bind(builder.Configuration.GetSection(BadConfiguration.Position))
    .ValidateDataAnnotations()
    .ValidateWithTsa();

/****************************************/
/*                tsa                   */
/****************************************/

// configuration (singleton)
DatabaseConfiguration databaseConfiguration = new DatabaseConfiguration();
builder.Configuration.GetSection(DatabaseConfiguration.Position).Bind(databaseConfiguration);
builder.Services.AddSingleton(databaseConfiguration);

// configuration (ioptions)
builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection(ApplicationOptions.Position));

// configuration (snapshot)
builder.Services.Configure<BadConfiguration>(builder.Configuration.GetSection(BadConfiguration.Position));

// library configuration (snapshot)
builder.Services.Configure<LibraryConfiguration>(builder.Configuration.GetSection(LibraryConfiguration.Position));

/****************************************/
/*                tsa                   */
/****************************************/

builder.Services.AddTerribleSettingsAuditor(builder.Configuration, s => {
    s.Screen = true;
    s.AbortScreenFailure = true;
});

var app = builder.Build();

/****************************************/
/*                tsa                   */
/****************************************/

await app.UseTerribleSettingsAuditorAsync(args);

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

try
{
    await app.RunAsync();
} catch (Exception)
{
    throw;
}

public partial class Program
{

}