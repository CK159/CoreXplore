using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db
{
	internal class RequestLogConfiguration : IEntityTypeConfiguration<RequestLog>
	{
		public void Configure(EntityTypeBuilder<RequestLog> builder)
		{
			builder.ToTable("RequestLog", "log");
			//builder.Property(b => b.RequestLogId);
			builder.Property(b => b.DateCreated).IsRequired().HasDefaultValueSql("getdate()");
			builder.Property(b => b.URL).IsRequired().HasDefaultValue("").HasMaxLength(2048);
			builder.Property(b => b.RequestMethod).IsRequired().HasDefaultValue("").HasMaxLength(64);
			builder.Property(b => b.RequestContentType).IsRequired().HasDefaultValue("").HasMaxLength(256);
			builder.Property(b => b.RequestSize).IsRequired();
			builder.Property(b => b.RequestText).IsRequired().HasDefaultValue("");
			//builder.Property(b => b.ResponseStatus);
			builder.Property(b => b.ResponseContentType).HasDefaultValue("").HasMaxLength(256);
			//builder.Property(b => b.ResponseSize);
			builder.Property(b => b.ResponseMs).HasColumnType("decimal(18, 4)");
			builder.Property(b => b.ResponseType).HasDefaultValue(ResponseTypes.Unknown);
			builder.Property(b => b.ResponseText).HasDefaultValue("");
			builder.Property(b => b.IP).IsRequired().HasDefaultValue("").HasMaxLength(64);
			builder.Property(b => b.UserAgent).IsRequired().HasDefaultValue("").HasMaxLength(256);
			builder.Property(b => b.Referer).IsRequired().HasDefaultValue("").HasMaxLength(2048);
			builder.Property(b => b.Location).IsRequired().HasDefaultValue("").HasMaxLength(2048);
			builder.Property(b => b.RequestBegin).IsRequired();
		}
	}

	public class RequestLog
	{
		public int RequestLogId { get; set; }

		/// <summary>
		/// The database server time the entry was recorded
		/// </summary>
		public DateTime DateCreated { get; set; }

		/// <summary>
		/// The web server time the request was started
		/// </summary>
		public DateTime RequestBegin { get; set; }

		public string URL { get; set; }
		public string RequestMethod { get; set; }
		public string RequestContentType { get; set; }
		public int RequestSize { get; set; }
		public string RequestText { get; set; }
		public int? ResponseStatus { get; set; }
		public string ResponseContentType { get; set; }
		public int? ResponseSize { get; set; }
		public decimal? ResponseMs { get; set; }
		public ResponseTypes ResponseType { get; set; }
		public string ResponseText { get; set; }
		public string IP { get; set; }
		public string UserAgent { get; set; }
		public string Referer { get; set; }
		public string Location { get; set; }
	}

	public enum ResponseTypes : int
	{
		Unknown = 0,
		StaticFile = 1,
		Mvc = 2,
		Api = 3,
		DynamicFile = 4
	}
}
