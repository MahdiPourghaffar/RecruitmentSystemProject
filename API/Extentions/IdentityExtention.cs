using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace API.Extentions
{
    public static class IdentityExtention
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
        {
            var _siteSetting = config.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
            services.AddIdentity<User, Role>(option =>
                {
                    option.Password.RequireDigit = _siteSetting.IdentitySettings.PasswordRequireDigit;
                    option.Password.RequiredLength = _siteSetting.IdentitySettings.PasswordRequiredLength;
                    option.Password.RequireNonAlphanumeric = _siteSetting.IdentitySettings.PasswordRequireNonAlphanumeric; //#@!
                    option.Password.RequireUppercase = _siteSetting.IdentitySettings.PasswordRequireUppercase;
                    option.Password.RequireLowercase = _siteSetting.IdentitySettings.PasswordRequireLowercase;
                    option.User.RequireUniqueEmail = _siteSetting.IdentitySettings.RequireUniqueEmail;
                })
                .AddEntityFrameworkStores<DataContext>()
                .AddRoles<Role>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
