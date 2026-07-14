using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Application.Services;
using ProjetCagnotte.Domain.Entities;
using ProjetCagnotte.Infrastructure.Data;
using ProjetCagnotte.Infrastructure.FileStorage;
using ProjetCagnotte.Infrastructure.Repositories;
using ProjetCagnotte.Infrastructure.Security;


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

            //Automaticly registered in DI with "AddIdentity":
            //UserManager < ApplicationUser >
            //SignInManager < ApplicationUser >
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();



            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<IFileStorageService,LocalFileStorageService>();


            return services;

        }




    }
}
