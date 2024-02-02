using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCRMLead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Consent",
                table: "LeadHead",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivacyShown",
                table: "LeadHead",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PrivacyInfo",
                table: "LeadHead",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LeadHeadId",
                table: "LeadDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_LeadDetails_LeadHeadId",
                table: "LeadDetails",
                column: "LeadHeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadDetails_LeadHead_LeadHeadId",
                table: "LeadDetails",
                column: "LeadHeadId",
                principalTable: "LeadHead",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadDetails_LeadHead_LeadHeadId",
                table: "LeadDetails");

            migrationBuilder.DropIndex(
                name: "IX_LeadDetails_LeadHeadId",
                table: "LeadDetails");

            migrationBuilder.DropColumn(
                name: "Consent",
                table: "LeadHead");

            migrationBuilder.DropColumn(
                name: "IsPrivacyShown",
                table: "LeadHead");

            migrationBuilder.DropColumn(
                name: "PrivacyInfo",
                table: "LeadHead");

            migrationBuilder.DropColumn(
                name: "LeadHeadId",
                table: "LeadDetails");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Applications");
        }
    }
}
