using Microsoft.EntityFrameworkCore;

namespace Db
{
	/// <summary>
	/// Context class specifically for usage with dependency injection with transient scoping
	/// </summary>
	public class DbcTransient : Dbc
	{
		public DbcTransient(DbContextOptions<Dbc> options) : base(options)
		{
		}
	}
}
