using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Regenerated_Partner4560 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Countries_CountryCodeId",
                table: "Partners");

            migrationBuilder.RenameColumn(
                name: "CountryCodeId",
                table: "Partners",
                newName: "CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Partners_CountryCodeId",
                table: "Partners",
                newName: "IX_Partners_CurrencyId");

            migrationBuilder.DropPrimaryKey(
            name: "PK_Partners",
            table: "Partners");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Partners",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "BusinessRegNo",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneCode",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
            name: "PK_Partners",
            table: "Partners",
            column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_CRMCurrencies_CurrencyId",
                table: "Partners",
                column: "CurrencyId",
                principalTable: "CRMCurrencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_CRMCurrencies_CurrencyId",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "BusinessRegNo",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "PhoneCode",
                table: "Partners");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "Partners",
                newName: "CountryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Partners_CurrencyId",
                table: "Partners",
                newName: "IX_Partners_CountryCodeId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Partners",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_Countries_CountryCodeId",
                table: "Partners",
                column: "CountryCodeId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
