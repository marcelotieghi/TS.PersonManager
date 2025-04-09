using Microsoft.Extensions.DependencyInjection;
using TS.PersonManager.Business.Business;
using TS.PersonManager.Business.Interface;

namespace TS.PersonManager.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddScoped<IPersonBusiness, PersonBusiness>();
        services.AddScoped<IValidateUserBusiness, ValidateUserBusiness>();

        return services;
    }
}