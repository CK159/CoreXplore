using Microsoft.EntityFrameworkCore;

namespace Db
{
	/// <summary>
	/// Context class specifically for usage with dependency injection with transient scoping
	/// </summary>
	public class DbCoreTransient : DbCore
	{
		public DbCoreTransient(DbContextOptions<DbCore> options) : base(options)
		{
		}
	}
}
