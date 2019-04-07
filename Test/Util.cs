using System.IO;
using Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Test
{
	public class Util
	{
		public static IConfiguration Config { get; } = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", false, true)
			//.AddJsonFile($"appsettings.{Env}.json", true, true)
			.AddJsonFile("appsettings.Private.json", true, true)
			//.AddEnvironmentVariables()
			.Build();

		public static DbCore BuildContext(IConfiguration config = null)
		{
			if (config == null)
			{
				config = Config;
			}
			
			DbContextOptionsBuilder<DbCore> builder = new DbContextOptionsBuilder<DbCore>();
			builder.UseSqlServer(config.GetConnectionString("UnitTestConnection"));
			return new DbCore(builder.Options);
		}
	}
}
