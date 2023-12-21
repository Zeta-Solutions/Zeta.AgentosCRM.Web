using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Some_Missing_Fields_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Partners_PartnerId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Agents_AgentId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "WorkflowNae",
                table: "ClientQuotationDetails",
                newName: "WorkflowName");

            migrationBuilder.AddColumn<long>(
                name: "ApplicationId",
                table: "Notes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ApplicationStageId",
                table: "Notes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "CRMTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "AgentId",
                table: "Clients",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "TrainingRequired",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProductCount",
                table: "ClientQuotationHeads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "ClientQuotationHeads",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                table: "ClientEducations",
                type: "bigint",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsGpa",
                table: "ClientEducations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "PartnerId",
                table: "Applications",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "Applications",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ApplicationId",
                table: "Notes",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ApplicationStageId",
                table: "Notes",
                column: "ApplicationStageId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEducations_ClientId",
                table: "ClientEducations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_BranchId",
                table: "Applications",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Branches_BranchId",
                table: "Applications",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Partners_PartnerId",
                table: "Applications",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientEducations_Clients_ClientId",
                table: "ClientEducations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Agents_AgentId",
                table: "Clients",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_ApplicationStages_ApplicationStageId",
                table: "Notes",
                column: "ApplicationStageId",
                principalTable: "ApplicationStages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Applications_ApplicationId",
                table: "Notes",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Branches_BranchId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Partners_PartnerId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientEducations_Clients_ClientId",
                table: "ClientEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Agents_AgentId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_ApplicationStages_ApplicationStageId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Applications_ApplicationId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ApplicationId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ApplicationStageId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_ClientEducations_ClientId",
                table: "ClientEducations");

            migrationBuilder.DropIndex(
                name: "IX_Applications_BranchId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ApplicationStageId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "CRMTasks");

            migrationBuilder.DropColumn(
                name: "TrainingRequired",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ProductCount",
                table: "ClientQuotationHeads");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "ClientQuotationHeads");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ClientEducations");

            migrationBuilder.DropColumn(
                name: "IsGpa",
                table: "ClientEducations");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "WorkflowName",
                table: "ClientQuotationDetails",
                newName: "WorkflowNae");

            migrationBuilder.AlterColumn<long>(
                name: "AgentId",
                table: "Clients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PartnerId",
                table: "Applications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Partners_PartnerId",
                table: "Applications",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Agents_AgentId",
                table: "Clients",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
