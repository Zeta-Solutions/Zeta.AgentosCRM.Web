using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Partner_Contract_PartnerId_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PartnerId",
                table: "PartnerContracts",
                type: "bigint",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PartnerContracts_PartnerId",
                table: "PartnerContracts",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerContracts_Partners_PartnerId",
                table: "PartnerContracts",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartnerContracts_Partners_PartnerId",
                table: "PartnerContracts");

            migrationBuilder.DropIndex(
                name: "IX_PartnerContracts_PartnerId",
                table: "PartnerContracts");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "PartnerContracts");
        }
    }
}
