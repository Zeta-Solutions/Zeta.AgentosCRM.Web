using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_WorkflowOffice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WorkflowSteps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApplicationIntakeRequired",
                table: "WorkflowSteps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNoteRequired",
                table: "WorkflowSteps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPartnerClientIdRequired",
                table: "WorkflowSteps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStartEndDateRequired",
                table: "WorkflowSteps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWinStage",
                table: "WorkflowSteps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsForAllOffices",
                table: "Workflows",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "WorkflowOffices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationUnitId = table.Column<long>(type: "bigint", nullable: true),
                    WorkflowId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_WorkflowOffices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowOffices_AbpOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkflowOffices_Workflows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowOffices_OrganizationUnitId",
                table: "WorkflowOffices",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowOffices_TenantId",
                table: "WorkflowOffices",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowOffices_WorkflowId",
                table: "WorkflowOffices",
                column: "WorkflowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkflowOffices");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WorkflowSteps");

            migrationBuilder.DropColumn(
                name: "IsApplicationIntakeRequired",
                table: "WorkflowSteps");

            migrationBuilder.DropColumn(
                name: "IsNoteRequired",
                table: "WorkflowSteps");

            migrationBuilder.DropColumn(
                name: "IsPartnerClientIdRequired",
                table: "WorkflowSteps");

            migrationBuilder.DropColumn(
                name: "IsStartEndDateRequired",
                table: "WorkflowSteps");

            migrationBuilder.DropColumn(
                name: "IsWinStage",
                table: "WorkflowSteps");

            migrationBuilder.DropColumn(
                name: "IsForAllOffices",
                table: "Workflows");
        }
    }
}
