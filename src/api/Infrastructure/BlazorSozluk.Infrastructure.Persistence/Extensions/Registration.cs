using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Infrastructure.Persistence.Context;
using BlazorSozluk.Api.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorSozluk.Api.Infrastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlazorSozlukContext>(conf =>
        {
            var conStr = configuration["BlazorSozlukDbConnectionString"].ToString();
            conf.UseSqlServer(conStr);
        });

        //var seedData = new Seed();
        //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
