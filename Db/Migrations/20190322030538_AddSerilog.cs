using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class AddSerilog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "log");

            migrationBuilder.RenameTable(
                name: "RequestLog",
                newName: "RequestLog",
                newSchema: "log");

            migrationBuilder.CreateTable(
                name: "AppLog",
                schema: "log",
                columns: table => new
                {
                    AppLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(nullable: true),
                    Level = table.Column<string>(maxLength: 128, nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Application = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLog", x => x.AppLogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppLog",
                schema: "log");

            migrationBuilder.RenameTable(
                name: "RequestLog",
                schema: "log",
                newName: "RequestLog");
        }
    }
}
