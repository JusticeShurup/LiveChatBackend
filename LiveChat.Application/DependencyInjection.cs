using LiveChat.Application.Services;
using LiveChat.Application.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LiveChat.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLogic(this IServiceCollection services)
        {

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddScoped<IUserService, UserService>();

            return services;
        }

    }
}
