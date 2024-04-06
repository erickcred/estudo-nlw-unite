using Microsoft.EntityFrameworkCore;
using PassIn.Api.StartDI;
using PassIn.Application.UseCases.Events.Get;
using PassIn.Application.UseCases.Events.Rgister;
using PassIn.Infrastructure;
using Serilog;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration Configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Logger logger = new LoggerConfiguration()
  .ReadFrom.Configuration(Configuration)
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

builder.Host.UseSerilog(logger, dispose: true);
builder.Host.ConfigureServices(services =>
{
  services.RegisterDbContexts(builder.Configuration);
  services.RegisterServices();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
