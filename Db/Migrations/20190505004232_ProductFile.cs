using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class ProductFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    FileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    Content = table.Column<byte[]>(nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.FileId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(nullable: true),
                    ProductDesc = table.Column<string>(nullable: true),
                    ProductRichDesc = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ProductResource",
                columns: table => new
                {
                    ProductResourceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: true),
                    ResourceName = table.Column<string>(nullable: true),
                    ResourceInfo = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    ResourceFileId = table.Column<int>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductResource", x => x.ProductResourceId);
                    table.ForeignKey(
                        name: "FK_ProductResource_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductResource_File_ResourceFileId",
                        column: x => x.ResourceFileId,
                        principalTable: "File",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductResource_ProductId",
                table: "ProductResource",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductResource_ResourceFileId",
                table: "ProductResource",
                column: "ResourceFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductResource");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "File");
        }
    }
}
