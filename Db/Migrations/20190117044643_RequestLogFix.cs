using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class RequestLogFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestText",
                table: "RequestLogs",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestText",
                table: "RequestLogs",
                nullable: true,
                oldClrType: typeof(string),
                oldDefaultValue: "");
        }
    }
}
