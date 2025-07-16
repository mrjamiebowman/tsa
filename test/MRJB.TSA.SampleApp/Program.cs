var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// tsa
builder.Services.AddTerribleSettingsAuditor(builder.Configuration, s =>
{
    s.Features.Validation = true;
    s.Features.Arguments = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

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