using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class FixProductFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductResource_Product_ProductId",
                table: "ProductResource");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductResource_File_ResourceFileId",
                table: "ProductResource");

            migrationBuilder.DropIndex(
                name: "IX_ProductResource_ResourceFileId",
                table: "ProductResource");

            migrationBuilder.DropColumn(
                name: "ResourceFileId",
                table: "ProductResource");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceName",
                table: "ProductResource",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResourceInfo",
                table: "ProductResource",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductResource",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "ProductResource",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "ProductResource",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "ProductResource",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ProductRichDesc",
                table: "Product",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Product",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductDesc",
                table: "Product",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Product",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Product",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "MimeType",
                table: "File",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "File",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "File",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "File",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_ProductResource_FileId",
                table: "ProductResource",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductResource_File_FileId",
                table: "ProductResource",
                column: "FileId",
                principalTable: "File",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductResource_Product_ProductId",
                table: "ProductResource",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductResource_File_FileId",
                table: "ProductResource");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductResource_Product_ProductId",
                table: "ProductResource");

            migrationBuilder.DropIndex(
                name: "IX_ProductResource_FileId",
                table: "ProductResource");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "ProductResource");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceName",
                table: "ProductResource",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ResourceInfo",
                table: "ProductResource",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductResource",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "ProductResource",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "ProductResource",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceFileId",
                table: "ProductResource",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductRichDesc",
                table: "Product",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Product",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProductDesc",
                table: "Product",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Product",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Product",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "MimeType",
                table: "File",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "File",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "File",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "File",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.CreateIndex(
                name: "IX_ProductResource_ResourceFileId",
                table: "ProductResource",
                column: "ResourceFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductResource_Product_ProductId",
                table: "ProductResource",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductResource_File_ResourceFileId",
                table: "ProductResource",
                column: "ResourceFileId",
                principalTable: "File",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
