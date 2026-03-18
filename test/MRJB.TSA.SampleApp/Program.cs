using Scalar.AspNetCore;
using TerribleSettingsAuditor.SampleApp.Configuration;

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

/****************************************/
/*                tsa                   */
/****************************************/

builder.Services.AddTerribleSettingsAuditor(builder.Configuration, s =>
{
    s.PreCheck = true;
    s.AbortPreCheckFailure = true;
});


var app = builder.Build();

/****************************************/
/*                tsa                   */
/****************************************/

await app.UseTerribleSettingsAuditor(args);

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