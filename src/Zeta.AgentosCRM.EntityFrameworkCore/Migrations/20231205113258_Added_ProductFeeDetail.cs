using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_ProductFeeDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductFeeDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    InstallmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Installments = table.Column<int>(type: "int", nullable: false),
                    TotalFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClaimTerms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionPer = table.Column<double>(type: "float", nullable: false),
                    IsPayable = table.Column<bool>(type: "bit", nullable: false),
                    AddInQuotation = table.Column<bool>(type: "bit", nullable: false),
                    FeeTypeId = table.Column<int>(type: "int", nullable: false),
                    ProductFeeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ProductFeeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFeeDetails_FeeTypes_FeeTypeId",
                        column: x => x.FeeTypeId,
                        principalTable: "FeeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFeeDetails_ProductFees_ProductFeeId",
                        column: x => x.ProductFeeId,
                        principalTable: "ProductFees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeeDetails_FeeTypeId",
                table: "ProductFeeDetails",
                column: "FeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeeDetails_ProductFeeId",
                table: "ProductFeeDetails",
                column: "ProductFeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeeDetails_TenantId",
                table: "ProductFeeDetails",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFeeDetails");
        }
    }
}
