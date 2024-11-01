
namespace MovieVault.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IMovieService, MovieService>();
            services.AddTransient<IEmailSender, EmailSender>();
            return services;
        }
    }
}
