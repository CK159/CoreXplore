using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db
{
	internal class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(b => b.ProductName).IsRequired().HasMaxLength(128);
			builder.Property(b => b.ProductDesc).IsRequired().HasDefaultValue("").HasMaxLength(1024);
			builder.Property(b => b.ProductRichDesc).IsRequired().HasDefaultValue("").HasMaxLength(2048);
			builder.Property(b => b.Active).IsRequired().HasDefaultValue(true);
			builder.Property(b => b.DateCreated).IsRequired().HasDefaultValueSql("getdate()");
		}
	}
	
	public class Product
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductDesc { get; set; }
		public string ProductRichDesc { get; set; }
		//public virtual ProductType Type { get; set; }
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }

		public virtual ICollection<ProductResource> ProductResources { get; set; }
	}
}
