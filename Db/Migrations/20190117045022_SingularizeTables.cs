using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class SingularizeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestLogs",
                table: "RequestLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "RequestLogs",
                newName: "RequestLog");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestLog",
                table: "RequestLog",
                column: "RequestLogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "MessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestLog",
                table: "RequestLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "RequestLog",
                newName: "RequestLogs");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestLogs",
                table: "RequestLogs",
                column: "RequestLogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "MessageId");
        }
    }
}
