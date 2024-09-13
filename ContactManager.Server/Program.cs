using ContactManager.Server.Extensions;
using ContactManager.Services.Model.Mapper;
using ContactManagerDB.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ContactManager.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddCors()
                            .AddEndpointsApiExplorer()
                            .AddSwaggerGen()
                            .AddDbContext<ContactDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")))
                            .AddAutoMapper(typeof(ContactMapperProfile))
                            .AddAppServices();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseCors(builder.Configuration["CORS"]);
            app.UseCors(
                opt => opt.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(result);
            }));

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
