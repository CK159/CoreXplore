using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db
{
	internal class StoreConfiguration : IEntityTypeConfiguration<Store>
	{
		public void Configure(EntityTypeBuilder<Store> builder)
		{
			builder.Property(b => b.StoreName).IsRequired().HasMaxLength(128);
			builder.Property(b => b.ImportantConfigId).IsRequired().HasDefaultValue(7);
			builder.Property(b => b.Description).IsRequired().HasDefaultValue("").HasMaxLength(1024);
			builder.Property(b => b.Owner).IsRequired().HasDefaultValue("").HasMaxLength(128);
			builder.Property(b => b.Active).IsRequired().HasDefaultValue(true).ValueGeneratedNever(); //Avoid dumb EF warning about default values
			builder.Property(b => b.DateCreated).IsRequired().HasDefaultValueSql("getdate()");
		}
	}
	
	
	public class Store
	{
		public int StoreId { get; set; }
		public string StoreName { get; set; }
		//public virtual StoreStatus Status { get; set; }
		public int ImportantConfigId { get; set; }
		public string Description { get; set; }
		public string Owner { get; set; }
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
