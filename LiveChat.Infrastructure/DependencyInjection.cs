using LiveChat.Application.Repository;
using LiveChat.Infrastructure.Data;
using LiveChat.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LiveChat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<LiveChatDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
