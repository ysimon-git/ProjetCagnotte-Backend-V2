using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Infrastructure.Data;
using ProjetCagnotte.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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

            return services;

        }




    }
}
