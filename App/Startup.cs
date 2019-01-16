using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            
            services.AddDbContext<Dbc>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("I don't even know. Just give me the data, okay?")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAntiforgery(opts => opts.Cookie.Name = "_PHP_XSRF_");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            app.UseStaticFiles();
            app.UseCookiePolicy();

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