using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MultiTenantApp.Infrastructure.Common.Configurations;
using MultiTenantApp.Domain.Entities;
using System.Text;
using MultiTenantApp.Infrastructure.Persistence;
using MultiTenantApp.Application.Common.Interfaces;
using MultiTenantApp.Infrastructure.Services;
using MultiTenantApp.TenantHost.Persistence;
using MultiTenantApp.TenantHost.Interfaces;
using MultiTenantApp.TenantHost.Services;
using MultiTenantApp.Infrastructure.Common.Factories;
using MultiTenantApp.Application.Common.Constants;

namespace MultiTenantApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions();
            services.Configure<JwtConfiguration>(config.GetSection(JwtConfiguration.SECTION_NAME));

            services.AddHttpClient();
            services.AddHttpContextAccessor();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString(AppConstant.BaseTenantDbConnection),
                    sqlServerOptions => sqlServerOptions.CommandTimeout(300));
            });

            services.AddDbContext<TenantDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString(AppConstant.CatalogDbConnection),
                    sqlServerOptions => sqlServerOptions.CommandTimeout(300));
            });

            // For Identity
            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = config[AppConstant.JwtAudience],
                    ValidIssuer = config[AppConstant.JwtIssuer],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config[AppConstant.JwtKey]))
                };
            });

            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            services.AddScoped<DbContextFactory>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddScoped<IDbMigrationService, DbMigrationService>();

            return services;
        }
    }
}
