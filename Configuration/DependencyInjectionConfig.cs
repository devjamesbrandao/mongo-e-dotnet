using Microsoft.Extensions.Options;
using Mongo.Data.Configurations;
using Mongo.Data.Repositories;

namespace Mongo.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.Configure<DatabaseConfig>(configuration.GetSection(nameof(DatabaseConfig)));

            services.AddSingleton<IDatabaseConfig>(sp => sp.GetRequiredService<IOptions<DatabaseConfig>>().Value);

            services.AddSingleton<ITarefasRepository, TarefasRepository>();

            return services;
        }
    }
}