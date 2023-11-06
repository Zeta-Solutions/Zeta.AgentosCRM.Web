using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Regenerated_Client2858 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Countries_CountryCodeId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "CountryCodeId",
                table: "Clients",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_CountryCodeId",
                table: "Clients",
                newName: "IX_Clients_CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Countries_CountryId",
                table: "Clients",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Countries_CountryId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Clients",
                newName: "CountryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_CountryId",
                table: "Clients",
                newName: "IX_Clients_CountryCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Countries_CountryCodeId",
                table: "Clients",
                column: "CountryCodeId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
