using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MyExpenses.Api.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExpenses.Api
{
    public class Configuration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services
                .AddMvcCore(config => config.Filters.Add(typeof(ValidModelStateFilter)))
                .AddJsonFormatters()
                .AddApiExplorer()
                .AddCors(options =>
                {
                    // this defines a CORS policy called "default"
                    options.AddPolicy("default", policy =>
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
                })
                //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddSpeakerValidator>())
                .Services;
        }

        public static IApplicationBuilder Configure(
            IApplicationBuilder app,
            Func<IApplicationBuilder, IApplicationBuilder> configureHost) =>
            configureHost(app)
                .UseMvc(routes => routes.MapRoute("swagger", "{controller=docs}/{action=Swagger}"));
    }
}
