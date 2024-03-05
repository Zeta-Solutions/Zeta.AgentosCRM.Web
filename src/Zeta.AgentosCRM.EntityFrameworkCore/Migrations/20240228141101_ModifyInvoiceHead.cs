using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class ModifyInvoiceHead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInvoiceNetOrGross",
                table: "InvoiceHead");

            migrationBuilder.AddColumn<string>(
                name: "ClientEmail",
                table: "InvoiceHead",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceCreatedDate",
                table: "InvoiceHead",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceType",
                table: "InvoiceHead",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalRevenue",
                table: "InvoiceHead",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientEmail",
                table: "InvoiceHead");

            migrationBuilder.DropColumn(
                name: "InvoiceCreatedDate",
                table: "InvoiceHead");

            migrationBuilder.DropColumn(
                name: "InvoiceType",
                table: "InvoiceHead");

            migrationBuilder.DropColumn(
                name: "TotalRevenue",
                table: "InvoiceHead");

            migrationBuilder.AddColumn<bool>(
                name: "IsInvoiceNetOrGross",
                table: "InvoiceHead",
                type: "bit",
                nullable: true);
        }
    }
}
