using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyExpenses.Api.Infrastructure.HttpErros;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MyExpenses.Host.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHttpErrorFactory httpErrorFactory;
        private readonly ILogger<DefaultLoggerCategory> logger;
        private readonly TelemetryClient telemetryClient;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            IHttpErrorFactory httpErrorFactory,
            ILogger<DefaultLoggerCategory> logger,
            TelemetryClient telemetryClient)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.httpErrorFactory = httpErrorFactory ?? throw new ArgumentNullException(nameof(httpErrorFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            // Refine excptions control. Manage NotFoundException, etc
            catch (Exception exception)
            {
                telemetryClient.TrackException(exception);
                logger.LogError(exception.HResult, exception, exception.Message);
                await CreateHttpError(context, exception);
            }
        }

        private async Task CreateHttpError(HttpContext context, Exception exception)
        {
            var error = httpErrorFactory.CreateFrom(exception);

            await WriteResponseAsync(
                context,
                JsonConvert.SerializeObject(error),
                "application/json",
                error.Status);
        }

        private Task WriteResponseAsync(
           HttpContext context,
           string content,
           string contentType,
           int statusCode)
        {
            context.Response.Headers["Content-Type"] = new[] { contentType };
            context.Response.Headers["Cache-Control"] = new[] { "no-cache, no-store, must-revalidate" };
            context.Response.Headers["Pragma"] = new[] { "no-cache" };
            context.Response.Headers["Expires"] = new[] { "0" };
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(content);
        }
    }
}
