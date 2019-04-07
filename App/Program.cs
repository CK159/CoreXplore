using System;
using System.IO;
using App.Util;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace App
{
	public class Program
	{
		public static string Env => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
		
		public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
			.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Config"))
			.AddJsonFile("appsettings.json", false, true)
			.AddJsonFile($"appsettings.{Env}.json", true, true)
			.AddJsonFile("appsettings.Private.json", true, true)
			.AddEnvironmentVariables()
			.Build();

		public static int Main(string[] args)
		{
			Serilog.Debugging.SelfLog.Enable(Console.Error);

			LoggerConfiguration config = new LoggerConfiguration()
				.ReadFrom.Configuration(Configuration);

			//Email logging setup
			EmailConfiguration emailConfig = new EmailConfiguration();
			Configuration.Bind("SerilogEmail", emailConfig);
			
			if (emailConfig.Enabled)
			{
				config.WriteTo.Email(emailConfig.ToEmailConnectionInfo(Env),
					outputTemplate: emailConfig.OutputTemplate,
					batchPostingLimit: emailConfig.BatchPostingLimit,
					restrictedToMinimumLevel: emailConfig.RestrictedToMinimumLevel
				);
			}

			Log.Logger = config.CreateLogger();

			try
			{
				AppLogging
					.ForCategory("Lifecycle")
					.Information("Startup of {Application} version {Version} built {BuildDate}",
						ApplicationInfo.ApplicationName,
						ApplicationInfo.ApplicationVersion,
						ApplicationInfo.ApplicationBuildDate);

				CreateWebHostBuilder(args).Build().Run();

				AppLogging
					.ForCategory("Lifecycle")
					.Information("Shutdown of {Application}", ApplicationInfo.ApplicationName);

				return 0;
			}
			catch (Exception ex)
			{
				AppLogging
					.ForCategory("Lifecycle")
					.Fatal(ex, "Application {Application} terminated unexpectedly", ApplicationInfo.ApplicationName);

				return 1;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseKestrel(k => k.AddServerHeader = false)
				.ConfigureAppConfiguration(c =>
				{
					c.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Config"));
					c.AddJsonFile("appsettings.private.json", true, true);
				})
				.UseSerilog()
				.UseStartup<Startup>();
	}
}
