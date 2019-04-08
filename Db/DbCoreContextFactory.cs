using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Db
{
	public class DbCoreContextFactory : IDesignTimeDbContextFactory<DbCore>
	{
		public DbCore CreateDbContext(string[] args)
		{
			IConfiguration config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.DesignTime.json", false, true)
				.Build();
		
			DbContextOptionsBuilder<DbCore> optionsBuilder = new DbContextOptionsBuilder<DbCore>();
			optionsBuilder.UseSqlServer(config.GetConnectionString("DesignTimeConnection"));

			return new DbCore(optionsBuilder.Options);
		}
	}
}
