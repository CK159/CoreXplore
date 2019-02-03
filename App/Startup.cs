using System;
using System.Collections.Generic;
using System.IO;
using App.Util;
using Db;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

//Allows Rider/Resharper to provide IDE assistance for views in custom location
[assembly: AspMvcViewLocationFormat("/MvcPages/{1}/{0}.cshtml")]

namespace App
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			//Normal context
			services.AddDbContext<DbCore>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("I don't even know. Just give me the data, okay?")));
			//Transient-scoped context primarily for usage in isolating request logging from normal database activity 
			services.AddDbContext<DbCoreTransient>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("I don't even know. Just give me the data, okay?")),
				ServiceLifetime.Transient);
			//Identity context
			services.AddDbContext<IdentityCoreContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("I don't even know. Just give me the data, okay?")));

			services.AddDefaultIdentity<IdentityUser>()
				//.AddDefaultUI(UIFramework.Bootstrap3)
				.AddEntityFrameworkStores<IdentityCoreContext>();

			//Look for views in MvcPages folder
			services.Configure<RazorViewEngineOptions>(o =>
			{
				o.ViewLocationFormats.Add("~/MvcPages/{1}/{0}" + RazorViewEngine.ViewExtension);
			});

			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
				.AddRazorPagesOptions(options =>
				{
					options.AllowAreas = true;
					options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
					options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
				});

			services.AddAntiforgery(opts => opts.Cookie.Name = "_PHP_XSRF_");
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseRequestLogging();

			if(env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.Use((context, next) =>
			{
				//State of the art circa 2011
				//Includes support for Heartbleed
				context.Response.Headers["Server"] = "Apache 2.2.3 (CentOS) OpenSSL/1.0.1e PHP/5.3.5";
				context.Response.Headers["X-Powered-By"] = "PHP/5.3.5";
				return next.Invoke();
			});

			app.UseHttpsRedirection();

			//wwwroot default
			app.UseStaticFiles();

			//Serve *.js and *.css files in MvcPages folder
			app.UseStaticFiles(new StaticFileOptions
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

			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				//Default route for home page only
				routes.MapRoute(name: "default", template: "/", defaults: new {controller = "Home", action = "Index"});
				//Full routes show url.com/controller/action.php
				routes.MapRoute(name: "lolphp", template: "{controller}/{action}.php/{id?}");
			});
		}
	}
}
