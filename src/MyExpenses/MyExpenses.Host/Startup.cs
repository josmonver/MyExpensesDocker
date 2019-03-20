using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MyExpenses.Api;

namespace MyExpenses.Host
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.ConfigureServices(services)
                .AddAuthorization()
                .AddCustomServices()
                .AddOpenApi()
                .AddIdentityServerAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Configuration.Configure(
                app,
                host => host
                    .UseIf(env.IsDevelopment(), appBuilder => appBuilder.UseDeveloperExceptionPage())
                    .UseCors("default")
                    .UseAuthentication()
                    .UseSwagger()
                    .UseSwaggerUI(setup =>
                    {
                        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Expenses Api");
                    })
            );
        }
    }
}
