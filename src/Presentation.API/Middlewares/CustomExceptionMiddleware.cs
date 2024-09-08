namespace Presentation.API.Middlewares
{
    using Application.Services.Responses;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Text.Json;

    [ExcludeFromCodeCoverage]
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionMiddleware> logger;
        private const string SomethingWentWrong = "Something went wrong.";

        public CustomExceptionMiddleware(
            RequestDelegate next,
            ILogger<CustomExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonSerializer.Serialize(new VATResponse(SomethingWentWrong));
                await context.Response.WriteAsync(result);
            }
        }
    }
}
