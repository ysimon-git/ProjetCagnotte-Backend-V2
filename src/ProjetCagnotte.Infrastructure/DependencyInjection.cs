using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Domain.Entities;
using ProjetCagnotte.Infrastructure.Data;
using ProjetCagnotte.Infrastructure.Repositories;


namespace ProjetCagnotte.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IContributionRepository,ContributionRepository>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            return services;

        }




    }
}
