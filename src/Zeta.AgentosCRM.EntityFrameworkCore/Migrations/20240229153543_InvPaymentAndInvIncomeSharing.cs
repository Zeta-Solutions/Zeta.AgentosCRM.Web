using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class InvPaymentAndInvIncomeSharing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceCreatedDateDet",
                table: "InvoiceHead",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InvIncomeSharing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    InvoiceHeadId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentsReceivedId = table.Column<long>(type: "bigint", nullable: true),
                    IncomeSharing = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsTax = table.Column<bool>(type: "bit", nullable: true),
                    Tax = table.Column<int>(type: "int", nullable: true),
                    TaxSettings = table.Column<int>(type: "int", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalIncludingTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrganizationUnitId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_InvIncomeSharing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvIncomeSharing_AbpOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvIncomeSharing_TaxSettings_TaxSettings",
                        column: x => x.TaxSettings,
                        principalTable: "TaxSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvPaymentReceived",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    InvoiceHeadId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentsReceived = table.Column<long>(type: "bigint", nullable: true),
                    PaymentsReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MarkInvoicePaid = table.Column<bool>(type: "bit", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    AddNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_InvPaymentReceived", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvIncomeSharing_OrganizationUnitId",
                table: "InvIncomeSharing",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_InvIncomeSharing_TaxSettings",
                table: "InvIncomeSharing",
                column: "TaxSettings");

            migrationBuilder.CreateIndex(
                name: "IX_InvIncomeSharing_TenantId",
                table: "InvIncomeSharing",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_InvPaymentReceived_TenantId",
                table: "InvPaymentReceived",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvIncomeSharing");

            migrationBuilder.DropTable(
                name: "InvPaymentReceived");

            migrationBuilder.DropColumn(
                name: "InvoiceCreatedDateDet",
                table: "InvoiceHead");
        }
    }
}
