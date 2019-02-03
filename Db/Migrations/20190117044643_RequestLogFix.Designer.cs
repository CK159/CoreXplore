﻿// <auto-generated />
using System;
using Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Db.Migrations
{
    [DbContext(typeof(DbCore))]
    [Migration("20190117044643_RequestLogFix")]
    partial class RequestLogFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Db.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.HasKey("MessageId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Db.RequestLog", b =>
                {
                    b.Property<int>("RequestLogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("IP")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(64)
                        .HasDefaultValue("");

                    b.Property<string>("Location")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2048)
                        .HasDefaultValue("");

                    b.Property<string>("Referer")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2048)
                        .HasDefaultValue("");

                    b.Property<string>("RequestContentType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(256)
                        .HasDefaultValue("");

                    b.Property<string>("RequestMethod")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(64)
                        .HasDefaultValue("");

                    b.Property<string>("RequestText")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.Property<string>("ResponseContentType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(256)
                        .HasDefaultValue("");

                    b.Property<decimal>("ResponseMs")
                        .HasColumnType("decimal(18, 4)");

                    b.Property<int>("ResponseSize");

                    b.Property<int>("ResponseStatus");

                    b.Property<string>("ResponseText")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.Property<int>("ResponseType")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<string>("URL")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2048)
                        .HasDefaultValue("");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(256)
                        .HasDefaultValue("");

                    b.HasKey("RequestLogId");

                    b.ToTable("RequestLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
