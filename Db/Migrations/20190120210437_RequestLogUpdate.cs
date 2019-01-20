using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class RequestLogUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResponseText",
                table: "RequestLog",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ResponseStatus",
                table: "RequestLog",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ResponseSize",
                table: "RequestLog",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "ResponseMs",
                table: "RequestLog",
                type: "decimal(18, 4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 4)");

            migrationBuilder.AlterColumn<string>(
                name: "ResponseContentType",
                table: "RequestLog",
                maxLength: 256,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldDefaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestBegin",
                table: "RequestLog",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RequestSize",
                table: "RequestLog",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestBegin",
                table: "RequestLog");

            migrationBuilder.DropColumn(
                name: "RequestSize",
                table: "RequestLog");

            migrationBuilder.AlterColumn<string>(
                name: "ResponseText",
                table: "RequestLog",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ResponseStatus",
                table: "RequestLog",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResponseSize",
                table: "RequestLog",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ResponseMs",
                table: "RequestLog",
                type: "decimal(18, 4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResponseContentType",
                table: "RequestLog",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldDefaultValue: "");
        }
    }
}
