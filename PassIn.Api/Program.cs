using PassIn.Api.StartDI;
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

// DependencyInjections
builder.Services.RegisterDbContexts(builder.Configuration);
builder.Services.RegisterServices();

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
