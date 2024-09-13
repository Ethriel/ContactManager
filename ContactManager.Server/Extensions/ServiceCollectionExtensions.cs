using ContactManager.Services.Abstraction;
using ContactManager.Services.Implementation;
using ContactManagerDB.Model;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            return services.AddScoped<DbContext, ContactDbContext>()
                           .AddScoped(typeof(ICsvService<>), typeof(CsvService<>))
                           .AddScoped<IContactCrudService, ContactCrudService>()
                           .AddScoped<IContactCrudExtendedService, ContactCrudExtendedService>()
                           .AddScoped<IMapperService, MapperService>()
                           .AddScoped<IContactManagerService, ContactManagerService>();
        }
    }
}
