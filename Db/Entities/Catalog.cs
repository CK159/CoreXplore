using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db
{
	internal class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
	{
		public void Configure(EntityTypeBuilder<Catalog> builder)
		{
			builder.Property(b => b.CatalogName).IsRequired().HasMaxLength(128);
			builder.Property(b => b.CatalogDesc).IsRequired().HasDefaultValue("").HasMaxLength(1024);
			builder.Property(b => b.InternalName).IsRequired().HasDefaultValue("").HasMaxLength(16);
			builder.Property(b => b.Active).IsRequired();
			builder.Property(b => b.DateCreated).IsRequired().HasDefaultValueSql("getdate()");
		}
	}
	
	public class Catalog
	{
		public int CatalogId { get; set; }
		public string CatalogName { get; set; }
		public string CatalogDesc { get; set; }
		public string InternalName { get; set; }
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }

		public int StoreId { get; set; }
		public virtual Store Store { get; set; }
		public virtual ICollection<CatalogProduct> CatalogProducts { get; set; }
	}
}
