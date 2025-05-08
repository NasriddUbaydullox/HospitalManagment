using System.Reflection;
using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.Middleware;
using HospitalManagment_V2.Repository;
using HospitalManagment_V2.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace HospitalManagment_V2.Extansions
{

    public static class Extensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {



            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes()
                .Where(r => r.IsClass && r.Name.EndsWith("Repository"))
                .ToList();

            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces()
                    .FirstOrDefault(r => r.Name.EndsWith("Repository"));

                services.AddScoped(interfaceType, type);
            }

            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddMonitoring(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<Context>(options =>
            {
                options
                    .UseNpgsql("Server=localhost;Port=5432;Database=HospitalManagmentV2;User Id=postgres;Password=postgres;")
                    .LogTo(Console.WriteLine, LogLevel.Information);
            });
            return services;
        }
    }
}
