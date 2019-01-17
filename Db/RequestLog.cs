using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db
{
    internal class RequestLogConfiguration : IEntityTypeConfiguration<RequestLog>
    {
        public void Configure(EntityTypeBuilder<RequestLog> builder)
        {
            builder.Property(b => b.DateCreated).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(b => b.URL).IsRequired().HasDefaultValue("").HasMaxLength(2048);
            builder.Property(b => b.RequestMethod).IsRequired().HasDefaultValue("").HasMaxLength(64);
            builder.Property(b => b.RequestContentType).IsRequired().HasDefaultValue("").HasMaxLength(256);
            builder.Property(b => b.RequestText).IsRequired().HasDefaultValue("");
            builder.Property(b => b.ResponseStatus).IsRequired();
            builder.Property(b => b.ResponseContentType).IsRequired().HasDefaultValue("").HasMaxLength(256);
            builder.Property(b => b.ResponseSize).IsRequired();
            builder.Property(b => b.ResponseMs).IsRequired().HasColumnType("decimal(18, 4)");
            builder.Property(b => b.ResponseType).IsRequired().HasDefaultValue(ResponseTypes.Unknown);
            builder.Property(b => b.ResponseText).IsRequired().HasDefaultValue("");
            builder.Property(b => b.IP).IsRequired().HasDefaultValue("").HasMaxLength(64);
            builder.Property(b => b.UserAgent).IsRequired().HasDefaultValue("").HasMaxLength(256);
            builder.Property(b => b.Referer).IsRequired().HasDefaultValue("").HasMaxLength(2048);
            builder.Property(b => b.Location).IsRequired().HasDefaultValue("").HasMaxLength(2048);
        }
    }
    
    public class RequestLog
    {
        public int RequestLogId { get; set; }
        public DateTime DateCreated { get; set; }
        public string URL { get; set; }
        public string RequestMethod { get; set; }
        public string RequestContentType { get; set; }
        public string RequestText { get; set; }
        public int ResponseStatus { get; set; }
        public string ResponseContentType { get; set; }
        public int ResponseSize { get; set; }
        public decimal ResponseMs { get; set; }
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