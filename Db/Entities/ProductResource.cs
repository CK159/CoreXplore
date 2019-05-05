using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db
{
	internal class ProductResourceConfiguration : IEntityTypeConfiguration<ProductResource>
	{
		public void Configure(EntityTypeBuilder<ProductResource> builder)
		{
			builder.Property(b => b.ResourceName).IsRequired().HasMaxLength(128);
			builder.Property(b => b.ResourceInfo).IsRequired().HasDefaultValue("").HasMaxLength(1024);
			builder.Property(b => b.SortOrder).IsRequired();
			builder.Property(b => b.Active).IsRequired().HasDefaultValue(true);
			builder.Property(b => b.DateCreated).IsRequired().HasDefaultValueSql("getdate()");
			builder.Property(b => b.ProductId).IsRequired();
			builder.Property(b => b.FileId).IsRequired();
		}
	}
	
	public class ProductResource
	{
		public int ProductResourceId { get; set; }
		public string ResourceName { get; set; }
		public string ResourceInfo { get; set; }
		public int SortOrder { get; set; }
		//public virtual ResourceType Type { get; set; }
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }

		public int ProductId { get; set; }
		public virtual Product Product { get; set; }

		public int FileId { get; set; }
		public virtual File File { get; set; }
	}
}
