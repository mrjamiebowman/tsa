using MRJB.TSA.Core;
using MRJB.TSA.SampleApp.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// configuration
DatabaseConfiguration databaseConfiguration = new DatabaseConfiguration();
builder.Configuration.GetSection(DatabaseConfiguration.Position).Bind(databaseConfiguration);
builder.Services.AddSingleton(databaseConfiguration);

// tsa
builder.Services.AddTerribleSettingsAuditor(builder.Configuration, s =>
{
    s.PreCheck = true;
    s.AbortPreCheckFailure = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// terrible settings auditor (tsa)
app.UseTerribleSettingsAuditor(args);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

try
{
    await app.RunAsync();
} catch (Exception ex)
{
    throw;
}

public partial class Program
{

}