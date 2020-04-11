using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Havit.GoranG3.WebAPI.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Havit.GoranG3.Facades.Infrastructure.Security.Claims;

namespace Havit.GoranG3.WebAPI.Infrastructure.ConfigurationExtensions
{
    public static class AuthenticationConfig
    {
        public static void AddCustomizedAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Havit.GoranG3.WebAPI.Infrastructure.Security.JwtBearerSettings>(configuration.GetSection("AppSettings:JwtBearer"));
            JwtBearerSettings jwtBearerSettings = configuration.GetSection("AppSettings:JwtBearer").Get<Havit.GoranG3.WebAPI.Infrastructure.Security.JwtBearerSettings>();
			//OpenIdConnectSettings openIdConnectSettings = configuration.GetSection("AppSettings:OpenIdConnect").Get<Havit.GoranG3.WebAPI.Infrastructure.Security.OpenIdConnectSettings>();

			var authenticationBuilder = services.AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme);

            authenticationBuilder = authenticationBuilder.AddJwtBearer(options =>
            {
                options.Authority = jwtBearerSettings.Authority;
                options.Audience = jwtBearerSettings.Audience;
				options.RequireHttpsMetadata = false; // pro stage nemáme SSL certifikáty

			});

			//if (!String.IsNullOrEmpty(openIdConnectSettings.Authority))
			//{
			//	authenticationBuilder = authenticationBuilder
			//		.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
			//		{
			//			options.ResponseType = OpenIdConnectResponseType.Code;
			//			options.CallbackPath = "/callback";
			//			options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			//			options.Scope.Clear();

			//			options.Authority = openIdConnectSettings.Authority;
			//			options.ClientId = openIdConnectSettings.ClientId;
			//			options.Scope.Add(openIdConnectSettings.Scope);
			//			options.ClientSecret = openIdConnectSettings.ClientSecret;
			//			options.RequireHttpsMetadata = false;
			//		})
			//		.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
			//		{
			//			// Vestavěná metoda OnRedirectToLogin rozlišuje Ajaxové a ostatní requesty. Pro Ajaxové se vrací 401, pro ostatní 302 (Redirect). 
			//			// Rozlišení requestů je dáno existencí hlavičky X-Requested-With, avšak ta se v cross domain requestech neposílá.
			//			// Proto metodu nahrazujeme tak, aby vždy vracela 401.
			//			options.Events.OnRedirectToLogin = context =>
			//			{
			//				context.Response.StatusCode = 401;
			//				return Task.CompletedTask;
			//			};
			//			// Obdobně OnRedirectToAccessDenied (řeší situaci, kdy dojde k zamítnutí oprávnění v AuthorizeFilteru).
			//			options.Events.OnRedirectToAccessDenied = context =>
			//			{
			//				context.Response.StatusCode = 403;
			//				return Task.CompletedTask;
			//			};
			//		});
			//}
			// Pod IClaimsTransformation je standardně zaregistrováno NoopClaimsTransformation
			// Pokud přidáme naší vlastní službu, je tato až druhá a není tak resolvována (uff),
			// Proto tuto službu, kterou nechceme, odebereme (a použijeme naší službu).
			services.Remove(services.Where(item => item.ImplementationType == typeof(NoopClaimsTransformation)).Single());
			addCustomizedAuthenticationCalled = true;

        }
        private static bool addCustomizedAuthenticationCalled = false;

		public static string[] GetAuthenticationSchemes(IConfiguration configuration)
		{
			if (!addCustomizedAuthenticationCalled)
			{
				throw new InvalidOperationException("Nejdříve je třeba zavolat metodu AddCustomizedAuthentication.");
			}

			return new string[] { JwtBearerDefaults.AuthenticationScheme };

			//OpenIdConnectSettings openIdConnectSettings = configuration.GetSection("AppSettings:OpenIdConnect").Get<Havit.GoranG3.WebAPI.Infrastructure.Security.OpenIdConnectSettings>();

			//return String.IsNullOrEmpty(openIdConnectSettings.Authority)
			//	? new string[] { JwtBearerDefaults.AuthenticationScheme }
			//	: new string[] { JwtBearerDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme };
		}
	}
}
