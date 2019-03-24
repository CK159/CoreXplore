using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class LogLevelTinyint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Level",
                schema: "log",
                table: "AppLog",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Level",
                schema: "log",
                table: "AppLog",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(byte));
        }
    }
}
