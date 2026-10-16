using BuildingBlocks.Exceptions.Handler;

namespace Ordering.API;

public static class DI
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCarter();

        services.AddExceptionHandler<CustomExceptionHandler>();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();

        app.UseExceptionHandler(options => { });

        return app;
    }
}
