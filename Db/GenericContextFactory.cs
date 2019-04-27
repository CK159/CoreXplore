using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Db
{
	public class DbCoreContextFactory : GenericContextFactory<DbCore>
	{
	}

	public class IdentityCoreContextFactory : GenericContextFactory<IdentityCoreContext>
	{
	}

	public class GenericContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
	{
		public T CreateDbContext(string[] args)
		{
			IConfiguration config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.DesignTime.json", false, true)
				.Build();
		
			DbContextOptionsBuilder<T> optionsBuilder = new DbContextOptionsBuilder<T>();
			optionsBuilder.UseSqlServer(config.GetConnectionString("DesignTimeConnection"));

			return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);
		}
	}
}
