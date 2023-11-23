using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Regenerated_PaymentInvoiceType8613 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentInvoiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceTypeId = table.Column<int>(type: "int", nullable: true),
                    ManualPaymentDetailId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_PaymentInvoiceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentInvoiceTypes_InvoiceTypes_InvoiceTypeId",
                        column: x => x.InvoiceTypeId,
                        principalTable: "InvoiceTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentInvoiceTypes_ManualPaymentDetails_ManualPaymentDetailId",
                        column: x => x.ManualPaymentDetailId,
                        principalTable: "ManualPaymentDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInvoiceTypes_InvoiceTypeId",
                table: "PaymentInvoiceTypes",
                column: "InvoiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInvoiceTypes_ManualPaymentDetailId",
                table: "PaymentInvoiceTypes",
                column: "ManualPaymentDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInvoiceTypes_TenantId",
                table: "PaymentInvoiceTypes",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentInvoiceTypes");
        }
    }
}
