using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastracture
{
    public class JwtService : IJwtService
    {

        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;

        public JwtService(SignInManager<User> signInManager, IConfiguration configuration, ICacheService cacheService)
        {
            this.signInManager = signInManager;
            _configuration = configuration;
            _cacheService = cacheService;
        }

        public async Task<string> GenerateAsync(User user)
        {
            var _siteSetting = _configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
            var claims = await GetClaimsAsync(user);
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey));

            var token = new JwtSecurityToken(
                issuer: _siteSetting.JwtSettings.Issuer,
                audience: _siteSetting.JwtSettings.Audience,
                expires: DateTime.Now.AddMinutes(_siteSetting.JwtSettings.ExpirationMinutes),
                notBefore:DateTime.Now.AddMinutes(_siteSetting.JwtSettings.NotBeforeMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var finalToken = tokenHandler.WriteToken(token);

            _cacheService.Set(
                $"Token_{user.Id}",
                finalToken,
                TimeSpan.FromHours(3),
                TimeSpan.FromMinutes(15));


            return finalToken;
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            var result = await signInManager.ClaimsFactory.CreateAsync(user);
            var list = new List<Claim>(result.Claims);
            return list;
        }
    }
}
