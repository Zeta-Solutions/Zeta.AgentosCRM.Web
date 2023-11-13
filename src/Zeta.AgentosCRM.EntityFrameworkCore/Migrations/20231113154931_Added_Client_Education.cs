using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_Client_Education : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcdamicScore",
                table: "ClientEducations");

            migrationBuilder.RenameColumn(
                name: "InstitutionName",
                table: "ClientEducations",
                newName: "Institution");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CourseEndDate",
                table: "ClientEducations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AcadmicScore",
                table: "ClientEducations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "DegreeLevelId",
                table: "ClientEducations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClientEducations_DegreeLevelId",
                table: "ClientEducations",
                column: "DegreeLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientEducations_DegreeLevels_DegreeLevelId",
                table: "ClientEducations",
                column: "DegreeLevelId",
                principalTable: "DegreeLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientEducations_DegreeLevels_DegreeLevelId",
                table: "ClientEducations");

            migrationBuilder.DropIndex(
                name: "IX_ClientEducations_DegreeLevelId",
                table: "ClientEducations");

            migrationBuilder.DropColumn(
                name: "AcadmicScore",
                table: "ClientEducations");

            migrationBuilder.DropColumn(
                name: "DegreeLevelId",
                table: "ClientEducations");

            migrationBuilder.RenameColumn(
                name: "Institution",
                table: "ClientEducations",
                newName: "InstitutionName");

            migrationBuilder.AlterColumn<string>(
                name: "CourseEndDate",
                table: "ClientEducations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<decimal>(
                name: "AcdamicScore",
                table: "ClientEducations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
