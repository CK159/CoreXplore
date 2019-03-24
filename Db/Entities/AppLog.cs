using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db
{
	internal class AppLogConfiguration : IEntityTypeConfiguration<AppLog>
	{
		public void Configure(EntityTypeBuilder<AppLog> builder)
		{
			builder.ToTable("AppLog", "log");

			builder.Property(b => b.TimeStamp).HasColumnType("datetime");
		}
	}

	public class AppLog
	{
		[Key]
		public int AppLogId { get; set; }

		public string Message { get; set; }
		public string MessageTemplate { get; set; }
		public byte Level { get; set; }
		public DateTime TimeStamp { get; set; }

		//Custom column
		public string Category { get; set; }

		//Custom column
		public string Application { get; set; }

		public string Exception { get; set; }
		public string Properties { get; set; }
	}
}
