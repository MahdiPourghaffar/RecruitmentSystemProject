using API.MiddleWares;
using Application.Helpers;
using Application.Interfaces;
using Infrastracture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extentions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserAccessor, UserAccesor>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddSingleton<IAuthorizationHandler, AnnnouncemntCompanyAuthorizationHandler>();
            services.AddScoped<ICacheService, CacheService>();

            return services;
        }

    }
}
