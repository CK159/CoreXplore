using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db
{
	internal class FileConfiguration : IEntityTypeConfiguration<File>
	{
		public void Configure(EntityTypeBuilder<File> builder)
		{
			builder.Property(b => b.FileName).IsRequired().HasMaxLength(128);
			builder.Property(b => b.MimeType).IsRequired().HasDefaultValue("").HasMaxLength(64);
			builder.Property(b => b.FilePath).IsRequired().HasDefaultValue("").HasMaxLength(256);
			builder.Property(b => b.Content).IsRequired();
			builder.Property(b => b.DateCreated).IsRequired().HasDefaultValueSql("getdate()");
		}
	}
	
	public class File
	{
		public int FileId { get; set; }
		public string FileName { get; set; }
		public string MimeType { get; set; }
		public string FilePath { get; set; }
		public byte[] Content { get; set; }
		public DateTime DateCreated { get; set; }
		
		public ICollection<ProductResource> ProductResources { get; set; }
	}
}
