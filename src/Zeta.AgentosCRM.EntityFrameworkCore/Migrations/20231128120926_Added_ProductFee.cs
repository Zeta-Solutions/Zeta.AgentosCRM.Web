using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_ProductFee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AgentId",
                table: "Clients",
                type: "bigint",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "AppointmentInvitees",
                type: "bigint",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ProductFees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Installments = table.Column<int>(type: "int", nullable: false),
                    InstallmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClaimTerms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionPer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AddInQuotation = table.Column<bool>(type: "bit", nullable: false),
                    NetTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    InstallmentTypeId = table.Column<int>(type: "int", nullable: true),
                    FeeTypeId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_ProductFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFees_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFees_FeeTypes_FeeTypeId",
                        column: x => x.FeeTypeId,
                        principalTable: "FeeTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductFees_InstallmentTypes_InstallmentTypeId",
                        column: x => x.InstallmentTypeId,
                        principalTable: "InstallmentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AgentId",
                table: "Clients",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentInvitees_UserId",
                table: "AppointmentInvitees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFees_CountryId",
                table: "ProductFees",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFees_FeeTypeId",
                table: "ProductFees",
                column: "FeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFees_InstallmentTypeId",
                table: "ProductFees",
                column: "InstallmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFees_TenantId",
                table: "ProductFees",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentInvitees_AbpUsers_UserId",
                table: "AppointmentInvitees",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Agents_AgentId",
                table: "Clients",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentInvitees_AbpUsers_UserId",
                table: "AppointmentInvitees");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Agents_AgentId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "ProductFees");

            migrationBuilder.DropIndex(
                name: "IX_Clients_AgentId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentInvitees_UserId",
                table: "AppointmentInvitees");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppointmentInvitees");
        }
    }
}
