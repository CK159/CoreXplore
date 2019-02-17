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
		public static readonly Dictionary<string, string> DefaultStaticFileTypes =
			new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
			{
				{".js", "application/javascript"},
				{".css", "text/css"}
			};

		/// <summary>
		/// Enables css and js files to be served from the MvcPages folder to the website root.
		/// Ex: MvcPages\test\index.js is available at http://localhost/test/index.js
		/// File types and root path can be altered via customStaticFileTypes and customRootPath, respectively.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="customStaticFileTypes"></param>
		/// <param name="customRootPath"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseMvcPagesStaticFiles(this IApplicationBuilder app,
			Dictionary<string, string> customStaticFileTypes = null, string customRootPath = "")
		{
			string mvcPageDir = Path.Combine(Directory.GetCurrentDirectory(), "MvcPages");

			//Application will fail to start up if you try to add a static file directory which doesn't exist
			if (Directory.Exists(mvcPageDir))
			{
				app.UseStaticFiles(new StaticFileOptions
				{
					FileProvider = new PhysicalFileProvider(
						mvcPageDir),
					RequestPath = customRootPath,
					ContentTypeProvider = new FileExtensionContentTypeProvider(customStaticFileTypes ?? DefaultStaticFileTypes)
				});
			}

			return app;
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
