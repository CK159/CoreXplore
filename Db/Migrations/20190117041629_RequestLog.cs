using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class RequestLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestLogs",
                columns: table => new
                {
                    RequestLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    URL = table.Column<string>(maxLength: 2048, nullable: false, defaultValue: ""),
                    RequestMethod = table.Column<string>(maxLength: 64, nullable: false, defaultValue: ""),
                    RequestContentType = table.Column<string>(maxLength: 256, nullable: false, defaultValue: ""),
                    RequestText = table.Column<string>(nullable: true),
                    ResponseStatus = table.Column<int>(nullable: false),
                    ResponseContentType = table.Column<string>(maxLength: 256, nullable: false, defaultValue: ""),
                    ResponseSize = table.Column<int>(nullable: false),
                    ResponseMs = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    ResponseType = table.Column<int>(nullable: false, defaultValue: 0),
                    ResponseText = table.Column<string>(nullable: false, defaultValue: ""),
                    IP = table.Column<string>(maxLength: 64, nullable: false, defaultValue: ""),
                    UserAgent = table.Column<string>(maxLength: 256, nullable: false, defaultValue: ""),
                    Referer = table.Column<string>(maxLength: 2048, nullable: false, defaultValue: ""),
                    Location = table.Column<string>(maxLength: 2048, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogs", x => x.RequestLogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestLogs");
        }
    }
}
