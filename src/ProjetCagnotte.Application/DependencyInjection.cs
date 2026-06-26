using Microsoft.Extensions.DependencyInjection;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService , ProductService>();
            services.AddScoped<IContributionService, ContributionService>();
   
            return services;

        }



    }
}
