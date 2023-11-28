using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_ProductEnglishRequirement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "ProductOtherInformations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "ProductFees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "ProductAcadamicRequirements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ProductEnglishRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Listening = table.Column<double>(type: "float", nullable: false),
                    Reading = table.Column<double>(type: "float", nullable: false),
                    Writing = table.Column<double>(type: "float", nullable: false),
                    Speaking = table.Column<double>(type: "float", nullable: false),
                    TotalScore = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEnglishRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductEnglishRequirements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOtherInformations_ProductId",
                table: "ProductOtherInformations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFees_ProductId",
                table: "ProductFees",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAcadamicRequirements_ProductId",
                table: "ProductAcadamicRequirements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEnglishRequirements_ProductId",
                table: "ProductEnglishRequirements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEnglishRequirements_TenantId",
                table: "ProductEnglishRequirements",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAcadamicRequirements_Products_ProductId",
                table: "ProductAcadamicRequirements",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFees_Products_ProductId",
                table: "ProductFees",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOtherInformations_Products_ProductId",
                table: "ProductOtherInformations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAcadamicRequirements_Products_ProductId",
                table: "ProductAcadamicRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFees_Products_ProductId",
                table: "ProductFees");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOtherInformations_Products_ProductId",
                table: "ProductOtherInformations");

            migrationBuilder.DropTable(
                name: "ProductEnglishRequirements");

            migrationBuilder.DropIndex(
                name: "IX_ProductOtherInformations_ProductId",
                table: "ProductOtherInformations");

            migrationBuilder.DropIndex(
                name: "IX_ProductFees_ProductId",
                table: "ProductFees");

            migrationBuilder.DropIndex(
                name: "IX_ProductAcadamicRequirements_ProductId",
                table: "ProductAcadamicRequirements");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductOtherInformations");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductFees");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductAcadamicRequirements");
        }
    }
}
