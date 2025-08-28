using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PrestamoBancario.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registrar todos los handlers de MediatR en esta assembly
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}