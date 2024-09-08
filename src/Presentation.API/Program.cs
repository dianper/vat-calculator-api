namespace Presentation.API
{
    using Application.Services.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Presentation.API.Extensions;
    using Presentation.API.Middlewares;

    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://0.0.0.0:5000");

            // Add services to the container
            builder.Services.AddValidators();
            builder.Services.AddServices();

            builder.Services.AddControllers();

            builder.Services.AddVersioning();
            builder.Services.AddSwagger();

            var app = builder.Build();

            // Configure Exception Handling
            app.UseMiddleware<CustomExceptionMiddleware>();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }

    // This class is used for the Integration Tests
    public partial class Program
    {
    }
}
