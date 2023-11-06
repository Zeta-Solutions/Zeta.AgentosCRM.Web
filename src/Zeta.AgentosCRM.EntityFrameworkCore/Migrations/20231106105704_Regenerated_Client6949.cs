using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Regenerated_Client6949 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Countries_CountryId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CountryId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClientStatus",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PTETraining",
                table: "Clients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientStatus",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PTETraining",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CountryId",
                table: "Clients",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Countries_CountryId",
                table: "Clients",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
