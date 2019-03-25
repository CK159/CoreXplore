using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class AppLog2XML : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Properties",
                schema: "log",
                table: "AppLog",
                type: "xml",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Properties",
                schema: "log",
                table: "AppLog",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "xml",
                oldNullable: true);
        }
    }
}
