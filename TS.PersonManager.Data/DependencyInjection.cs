using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TS.PersonManager.Data.Context;

namespace TS.PersonManager.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PersonContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}