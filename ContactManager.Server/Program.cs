using ContactManager.Server.Extensions;
using ContactManager.Services.Model.Mapper;
using ContactManagerDB.Model;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddEndpointsApiExplorer()
                            .AddSwaggerGen()
                            .AddDbContext<ContactDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")))
                            .AddAutoMapper(typeof(ContactMapperProfile))
                            .AddAppServices();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
