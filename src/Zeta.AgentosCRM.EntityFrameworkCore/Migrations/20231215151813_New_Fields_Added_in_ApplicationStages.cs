using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class New_Fields_Added_in_ApplicationStages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMandatory",
                table: "WorkflowStepDocumentCheckLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ApplicationStages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "ApplicationStages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "ApplicationStages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscontinue",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMandatory",
                table: "WorkflowStepDocumentCheckLists");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ApplicationStages");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "ApplicationStages");

            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "ApplicationStages");

            migrationBuilder.DropColumn(
                name: "IsDiscontinue",
                table: "Applications");
        }
    }
}
