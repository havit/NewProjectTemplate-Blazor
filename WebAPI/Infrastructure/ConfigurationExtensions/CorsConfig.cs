using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Havit.GoranG3.WebAPI.Infrastructure.ConfigurationExtensions
{
    public static class CorsConfig
    {
        public static void AddCustomizedCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Havit.GoranG3.WebAPI.Infrastructure.Cors.CorsOptions>(configuration.GetSection("AppSettings:Cors"));
            services.AddCors();
        }

        public static void UseCustomizedCors(this IApplicationBuilder app, IOptions<Havit.GoranG3.WebAPI.Infrastructure.Cors.CorsOptions> corsOptions)
        {
            string[] allowedOrigins = corsOptions.Value.AllowedOrigins.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(item => item.Trim()).ToArray();
            app.UseCors(policy =>
            {
                policy.WithOrigins(allowedOrigins)
                    .WithHeaders("Accept", "Content-Type", "Origin", "Authorization")
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .SetPreflightMaxAge(TimeSpan.FromHours(1));
            });
        }
    }
}