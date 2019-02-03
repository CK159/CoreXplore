using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace App.Util
{
	public static class MvcPageConfig
	{
		/// <summary>
		/// Enables css and js files to be served from the MvcPages folder
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseMvcPagesStaticFiles(this IApplicationBuilder app)
		{
			return app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(
					Path.Combine(Directory.GetCurrentDirectory(), "MvcPages")),
				RequestPath = "",
				ContentTypeProvider = new FileExtensionContentTypeProvider(
					new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
					{
						{".js", "application/javascript"},
						{".css", "text/css"}
					})
			});
		}
		
		/// <summary>
		/// Adds MvcPages folder to list of view search locations
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddMvcPages(this IServiceCollection services)
		{
			return services.Configure<RazorViewEngineOptions>(o =>
			{
				o.ViewLocationFormats.Add("~/MvcPages/{1}/{0}" + RazorViewEngine.ViewExtension);
			});
		}
	}
}
