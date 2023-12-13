using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Regenerated_ProductFee5718 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFees_FeeTypes_FeeTypeId",
                table: "ProductFees");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFees_Products_ProductId",
                table: "ProductFees");

            migrationBuilder.DropIndex(
                name: "IX_ProductFees_FeeTypeId",
                table: "ProductFees");

            migrationBuilder.DropIndex(
                name: "IX_ProductFees_ProductId",
                table: "ProductFees");

            migrationBuilder.DropColumn(
                name: "AddInQuotation",
                table: "ProductFees");

            migrationBuilder.DropColumn(
                name: "FeeTypeId",
                table: "ProductFees");

            migrationBuilder.DropColumn(
                name: "InstallmentAmount",
                table: "ProductFees");

            migrationBuilder.DropColumn(
                name: "Installments",
                table: "ProductFees");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductFees");

            migrationBuilder.DropColumn(
                name: "TotalFee",
                table: "ProductFees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AddInQuotation",
                table: "ProductFees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FeeTypeId",
                table: "ProductFees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InstallmentAmount",
                table: "ProductFees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Installments",
                table: "ProductFees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "ProductFees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalFee",
                table: "ProductFees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_ProductFees_FeeTypeId",
                table: "ProductFees",
                column: "FeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFees_ProductId",
                table: "ProductFees",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFees_FeeTypes_FeeTypeId",
                table: "ProductFees",
                column: "FeeTypeId",
                principalTable: "FeeTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFees_Products_ProductId",
                table: "ProductFees",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
