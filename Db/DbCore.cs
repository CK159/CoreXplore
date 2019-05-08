using Microsoft.EntityFrameworkCore;

namespace Db
{
	public class DbCore : DbContext
	{
		public DbCore(DbContextOptions<DbCore> options) : base(options)
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
			modelBuilder.ApplyConfiguration(new AppLogConfiguration());
			modelBuilder.ApplyConfiguration(new CatalogConfiguration());
			modelBuilder.ApplyConfiguration(new CatalogProductConfiguration());
			modelBuilder.ApplyConfiguration(new FileConfiguration());
			modelBuilder.ApplyConfiguration(new MessageConfiguration());
			modelBuilder.ApplyConfiguration(new ProductConfiguration());
			modelBuilder.ApplyConfiguration(new ProductResourceConfiguration());
			modelBuilder.ApplyConfiguration(new RequestLogConfiguration());
			modelBuilder.ApplyConfiguration(new StoreConfiguration());
		}

		public DbSet<AppLog> AppLogs { get; set; }
		public DbSet<Catalog> Catalogs { get; set; }
		public DbSet<CatalogProduct> CatalogProducts { get; set; }
		public DbSet<File> Files { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductResource> ProductResources { get; set; }
		public DbSet<RequestLog> RequestLogs { get; set; }
		public DbSet<Store> Stores { get; set; }
	}
}
