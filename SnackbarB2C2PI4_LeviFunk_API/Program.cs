using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SnackbarB2C2PI4_LeviFunk_MVC.Data;
using System.Reflection;

namespace SnackbarB2C2PI4_LeviFunk_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SnackbarB2C2PI4_LeviFunk_API",
                    Description = "A .NET Core WebApi for documentation",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "More information",
                        Url = new Uri("https://github.com/ZuydUniversity/A2D1_B2C2_2022BP1"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Applied License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                // using System.Reflection
                // Adding api commentation documentation
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // Add dbcontext service
            builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("LibraryConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
}