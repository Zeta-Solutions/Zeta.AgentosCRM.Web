using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class CRMInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppInvoices");

            migrationBuilder.AlterColumn<string>(
                name: "Inputtype",
                table: "LeadDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);

            migrationBuilder.CreateTable(
                name: "InvoiceDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CommissionPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CommissionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tax = table.Column<int>(type: "int", nullable: true),
                    TaxSettings = table.Column<int>(type: "int", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IncomeType = table.Column<int>(type: "int", nullable: true),
                    InvoiceTypes = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InvoiceHeadId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InvoiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDetail_InvoiceTypes_InvoiceTypes",
                        column: x => x.InvoiceTypes,
                        principalTable: "InvoiceTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceDetail_TaxSettings_TaxSettings",
                        column: x => x.TaxSettings,
                        principalTable: "TaxSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceHead",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: true),
                    PartnerId = table.Column<long>(type: "bigint", nullable: true),
                    ApplicationId = table.Column<long>(type: "bigint", nullable: true),
                    PartnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientDOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartnerClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workflow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountGivenToClient = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CommissionClaimed = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetFeePaidToPartner = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ManualPaymentDetail = table.Column<int>(type: "int", nullable: false),
                    ManualPaymentDetails = table.Column<int>(type: "int", nullable: true),
                    NetFeeReceived = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    TotalPayables = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmountInclTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalDue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    IsInvoiceNetOrGross = table.Column<bool>(type: "bit", nullable: true),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientAssignee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationOwner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDetailCount = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_InvoiceHead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceHead_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceHead_CRMCurrencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CRMCurrencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceHead_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceHead_ManualPaymentDetails_ManualPaymentDetails",
                        column: x => x.ManualPaymentDetails,
                        principalTable: "ManualPaymentDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceHead_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_InvoiceTypes",
                table: "InvoiceDetail",
                column: "InvoiceTypes");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_TaxSettings",
                table: "InvoiceDetail",
                column: "TaxSettings");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_TenantId",
                table: "InvoiceDetail",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHead_ApplicationId",
                table: "InvoiceHead",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHead_ClientId",
                table: "InvoiceHead",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHead_CurrencyId",
                table: "InvoiceHead",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHead_ManualPaymentDetails",
                table: "InvoiceHead",
                column: "ManualPaymentDetails");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHead_PartnerId",
                table: "InvoiceHead",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHead_TenantId",
                table: "InvoiceHead",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDetail");

            migrationBuilder.DropTable(
                name: "InvoiceHead");

            migrationBuilder.AlterColumn<string>(
                name: "Inputtype",
                table: "LeadDetails",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "AppInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantLegalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantTaxNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInvoices", x => x.Id);
                });
        }
    }
}
