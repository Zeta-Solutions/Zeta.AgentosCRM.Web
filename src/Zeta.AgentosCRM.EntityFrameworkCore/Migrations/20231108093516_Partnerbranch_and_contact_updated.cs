using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Partnerbranch_and_contact_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PartnerId",
                table: "PartnerContacts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PartnerId",
                table: "Branches",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PartnerContacts_PartnerId",
                table: "PartnerContacts",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_PartnerId",
                table: "Branches",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Partners_PartnerId",
                table: "Branches",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerContacts_Partners_PartnerId",
                table: "PartnerContacts",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Partners_PartnerId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerContacts_Partners_PartnerId",
                table: "PartnerContacts");

            migrationBuilder.DropIndex(
                name: "IX_PartnerContacts_PartnerId",
                table: "PartnerContacts");

            migrationBuilder.DropIndex(
                name: "IX_Branches_PartnerId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "PartnerContacts");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "Branches");
        }
    }
}
