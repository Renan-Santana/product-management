using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductManagement.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    id = table.Column<short>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    cnpj = table.Column<string>(type: "TEXT", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    situation = table.Column<bool>(type: "INTEGER", nullable: false),
                    manufacturing_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    supplier_id = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_supplier_id",
                table: "product",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_supplier_cnpj",
                table: "supplier",
                column: "cnpj",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "supplier");
        }
    }
}
