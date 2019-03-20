using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using MyExpenses.Host.Middlewares;
using System;

namespace MyExpenses.Host.Filters
{
    public class HostStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.UseMiddleware<ErrorHandlerMiddleware>();
                next(builder);
            };
        }
    }
}
