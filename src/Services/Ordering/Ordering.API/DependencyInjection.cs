namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Add API specific services here
            //services.AddCarter();
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure middleware here
            //app.MapCarter();

            return app;
        }
    }
}
