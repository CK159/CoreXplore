using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db
{
	internal class CatalogProductConfiguration : IEntityTypeConfiguration<CatalogProduct>
	{
		public void Configure(EntityTypeBuilder<CatalogProduct> builder)
		{
			builder.Property(b => b.SortOrder).IsRequired().HasDefaultValue(0);
			builder.Property(b => b.DateCreated).IsRequired().HasDefaultValueSql("getdate()");
			builder.Property(b => b.CatalogId).IsRequired();
			builder.Property(b => b.ProductId).IsRequired();
		}
	}
	
	public class CatalogProduct
	{
		public int CatalogProductId { get; set; }
		public int SortOrder { get; set; }
		public DateTime DateCreated { get; set; }

		public int CatalogId { get; set; }
		public virtual Catalog Catalog { get; set; }
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }
	}
}
