using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class FixProductFile2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResourceInfo",
                table: "ProductResource",
                maxLength: 1024,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "ProductRichDesc",
                table: "Product",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "ProductDesc",
                table: "Product",
                maxLength: 1024,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Content",
                table: "File",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResourceInfo",
                table: "ProductResource",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProductRichDesc",
                table: "Product",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProductDesc",
                table: "Product",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Content",
                table: "File",
                nullable: true,
                oldClrType: typeof(byte[]));
        }
    }
}
