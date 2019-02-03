using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db
{
	internal class MessageConfiguration : IEntityTypeConfiguration<Message>
	{
		public void Configure(EntityTypeBuilder<Message> builder)
		{
			builder.Property(b => b.MessageText).HasDefaultValue("");
			builder.Property(b => b.DateCreated).HasDefaultValueSql("getdate()");
		}
	}

	public class Message
	{
		public int MessageId { get; set; }

		[Required]
		public string MessageText { get; set; }

		public DateTime DateCreated { get; set; }
	}
}
