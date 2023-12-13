using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Quotation_Added_new_Fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "ClientQuotationDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartnerName",
                table: "ClientQuotationDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "ClientQuotationDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowNae",
                table: "ClientQuotationDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkflowStepId",
                table: "ApplicationStages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationStages_WorkflowStepId",
                table: "ApplicationStages",
                column: "WorkflowStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationStages_WorkflowSteps_WorkflowStepId",
                table: "ApplicationStages",
                column: "WorkflowStepId",
                principalTable: "WorkflowSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationStages_WorkflowSteps_WorkflowStepId",
                table: "ApplicationStages");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationStages_WorkflowStepId",
                table: "ApplicationStages");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "ClientQuotationDetails");

            migrationBuilder.DropColumn(
                name: "PartnerName",
                table: "ClientQuotationDetails");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "ClientQuotationDetails");

            migrationBuilder.DropColumn(
                name: "WorkflowNae",
                table: "ClientQuotationDetails");

            migrationBuilder.DropColumn(
                name: "WorkflowStepId",
                table: "ApplicationStages");
        }
    }
}
