using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_Missing_ProfileIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_AppBinaryObjects_ProfilePictureId",
                table: "Partners");

            migrationBuilder.RenameColumn(
                name: "ProfileImageId",
                table: "Agents",
                newName: "ProfilePictureId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfilePictureId",
                table: "Partners",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProfilePictureId",
                table: "Products",
                column: "ProfilePictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_AppBinaryObjects_ProfilePictureId",
                table: "Partners",
                column: "ProfilePictureId",
                principalTable: "AppBinaryObjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AppBinaryObjects_ProfilePictureId",
                table: "Products",
                column: "ProfilePictureId",
                principalTable: "AppBinaryObjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_AppBinaryObjects_ProfilePictureId",
                table: "Partners");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AppBinaryObjects_ProfilePictureId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProfilePictureId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureId",
                table: "Agents",
                newName: "ProfileImageId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfilePictureId",
                table: "Partners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_AppBinaryObjects_ProfilePictureId",
                table: "Partners",
                column: "ProfilePictureId",
                principalTable: "AppBinaryObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
