using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public static class Seed
{
    public static IApplicationBuilder ApplyMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
}