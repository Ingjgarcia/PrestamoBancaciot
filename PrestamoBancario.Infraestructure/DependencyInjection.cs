using Microsoft.Extensions.DependencyInjection;
using PrestamoBancario.Domain.Constracts;
using PrestamoBancario.Infraestructure.Persistence;
using System.Reflection;

namespace PrestamoBancario.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

    }
}
