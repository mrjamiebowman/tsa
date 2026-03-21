using Scalar.AspNetCore;
using TerribleSettingsAuditor.SampleApp.Domain.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

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

builder.Services.AddTerribleSettingsAuditor(builder.Configuration, s =>
{
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