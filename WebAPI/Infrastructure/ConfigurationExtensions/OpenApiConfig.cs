using System;
using System.IO;
using Havit.GoranG3.Facades.Properties;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.GoranG3.WebAPI.Infrastructure.ConfigurationExtensions
{
    public static class OpenApiConfig
	{
        public static void AddCustomizedOpenApi(this IServiceCollection services)
        {
			services.AddOpenApiDocument(c =>
			{
				c.DocumentName = "current";
				c.Title = "GoranG3";
				c.Version = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(AssemblyInfo).Assembly.Location).ProductVersion;
			});
        }

        public static void UseCustomizedOpenApiSwaggerUI(this IApplicationBuilder app)
        {
			app.UseOpenApi();
			app.UseSwaggerUi3();
		}
    }
}
