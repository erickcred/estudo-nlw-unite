using Microsoft.EntityFrameworkCore;
using PassIn.Api.Filters;
using PassIn.Application.UseCases.Events.Get;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Application.UseCases.Events.Rgister;
using PassIn.Infrastructure;

namespace PassIn.Api.StartDI
{
  public static class DependencyInjection
  {
    public static void RegisterDbContexts(this IServiceCollection builder, IConfiguration configuration)
    {
      builder.AddDbContext<PassInContext>(options =>
      {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);
      });
    }

    public static void RegisterServices(this IServiceCollection services)
    {
      services.AddMvc(options =>
      {
        options.Filters.Add(typeof(ExceptionFilter));
      });
      
      services.AddScoped<RegisterEventUseCase>();
      services.AddScoped<GetEventByIdUseCase>();
      services.AddScoped<RegisterAttendeeOnEventUseCase>();
    }
  }
}
