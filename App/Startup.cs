using System;
using System.Collections.Generic;
using System.Reflection;
using App.MvcPages.RequestLog;
using App.Util;
using Db;
using GenericServices.Setup;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//Allows Rider/Resharper to provide IDE assistance for views in custom location
[assembly: AspMvcViewLocationFormat("/MvcPages/{1}/{0}.cshtml")]

namespace App
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public static string FriendlyDbName = "Unknown";
		
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			string connectionString = Configuration.GetConnectionString("I don't even know. Just give me the data, okay?");

			//Normal context
			services.AddDbContext<DbCore>(options =>
				options.UseSqlServer(connectionString));
			//Transient-scoped context primarily for usage in isolating request logging from normal database activity 
			services.AddDbContext<DbCoreTransient>(options =>
					options.UseSqlServer(connectionString),
				ServiceLifetime.Transient);
			//Identity context
			services.AddDbContext<IdentityCoreContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddDefaultIdentity<IdentityUser>()
				//.AddDefaultUI(UIFramework.Bootstrap3)
				.AddEntityFrameworkStores<IdentityCoreContext>();

			//Look for views in the MvcPages folder
			services.AddMvcPages();

			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
				.AddRazorPagesOptions(options =>
				{
					options.AllowAreas = true;
					options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
					options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
				});

			services.AddAntiforgery(opts => opts.Cookie.Name = "_PHP_XSRF_");

			services.GenericServicesSimpleSetup<DbCore>(Assembly.GetAssembly(typeof(Startup)));
			
			//Use older password hashing algorithm for compatibility with Identity non-core
			services.Configure<PasswordHasherOptions>(options => options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2);

			ConfigureDi(services);
			FindFriendlyDbName(connectionString);
		}

		//Register all the services and other dependency injection entities used in the app 
		private void ConfigureDi(IServiceCollection services)
		{
			services.AddSingleton(ApplicationInfo.BuildApplicationInfo());
			services.AddScoped<RequestLogService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseRequestLogging();

			if (env.IsDevelopment())
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

			//Serve *.js and *.css files from the MvcPages folder
			app.UseMvcPagesStaticFiles();

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

		private void FindFriendlyDbName(string connectionString)
		{
			foreach (KeyValuePair<string, string> kv in Configuration.GetSection("FriendlyDbNames").AsEnumerable(true))
			{
				if (connectionString.Contains(kv.Value, StringComparison.OrdinalIgnoreCase))
				{
					FriendlyDbName = kv.Key;
					break;
				}
			}
		}
	}
}
