using MyExpenses.Api.Infrastructure.HttpErros;
using Swashbuckle.AspNetCore.Swagger;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services
                .AddApplicationInsightsTelemetry()
                .AddSingleton<IHttpErrorFactory, DefaultHttpErrorFactory>();
            //.AddSingleton<ISpeakerService, SpeakersService>()
            //.AddSingleton<INotificationQueue, NotificationQueue>();

            return services;
        }

        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.DescribeAllParametersInCamelCase();
                setup.DescribeStringEnumsInCamelCase();
                setup.SwaggerDoc("v1", new Info
                {
                    Title = "MyExpenses API",
                    Version = "v1"
                });
            });

            return services;
        }

        public static IServiceCollection AddIdentityServerAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                 .AddJwtBearer("Bearer", options =>
                 {
                     options.Authority = "http://localhost:53099/"; // IdentityServer Host
                     options.RequireHttpsMetadata = false;

                     options.Audience = "ExpensesApi";
                 });

            return services;
        }
    }
}
