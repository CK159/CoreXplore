using Microsoft.EntityFrameworkCore;

namespace Db
{
	public class Dbc : DbContext
	{
		/*public Dbc()
		{
		}*/

		public Dbc(DbContextOptions<Dbc> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//Connection string now configured through Startup.cs
			//optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=CoreXplore;Integrated Security=true;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//By default, the table name is now the name of the DbSet property (pluralized)
			//This makes the table name the same as the entity (singularized)
			modelBuilder.RemovePluralizingTableNameConvention();

			//Fluent API configuration for all entities
			modelBuilder.ApplyConfiguration(new MessageConfiguration());
			modelBuilder.ApplyConfiguration(new RequestLogConfiguration());
		}

		public DbSet<Message> Messages { get; set; }
		public DbSet<RequestLog> RequestLogs { get; set; }
	}
}
