using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Db
{
	public static class ModelBuilderExtensions
	{
		//By default, the table name is now the name of the DbSet property (pluralized)
		//This makes the table name the same as the entity (singularized)
		//https://stackoverflow.com/a/37502978
		public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
		{
			foreach(IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
			{
				entity.Relational().TableName = entity.DisplayName();
			}
		}
	}
}
