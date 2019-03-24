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
		public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
				optional: true, reloadOnChange: true)
			.AddEnvironmentVariables()
			.Build();

		public static int Main(string[] args)
		{
			Serilog.Debugging.SelfLog.Enable(Console.Error);

			LoggerConfiguration config = new LoggerConfiguration()
				.ReadFrom.Configuration(Configuration)
				.Enrich.FromLogContext()
				.WriteTo.Console();

			Log.Logger = config.CreateLogger();

			try
			{
				AppLogging
					.ForCategory("Lifecycle")
					.Information("Starting web host {Application} version {Version}",
						"CoreXplore",
						System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);

				CreateWebHostBuilder(args).Build().Run();
				return 0;
			}
			catch (Exception ex)
			{
				AppLogging.ForCategory("Lifecycle").Fatal(ex, "Host terminated unexpectedly");
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
				.UseSerilog()
				.UseStartup<Startup>();
	}
}
