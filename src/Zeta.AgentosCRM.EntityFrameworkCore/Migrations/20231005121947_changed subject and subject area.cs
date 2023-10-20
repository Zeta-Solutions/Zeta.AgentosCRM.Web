using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class changedsubjectandsubjectarea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAreas_Subjects_SubjectId",
                table: "SubjectAreas");

             
            migrationBuilder.DropIndex(
                name: "IX_SubjectAreas_SubjectId",
                table: "SubjectAreas");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "SubjectAreas");

            migrationBuilder.AddColumn<int>(
                name: "SubjectAreaId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectAreaId",
                table: "Subjects",
                column: "SubjectAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_SubjectAreas_SubjectAreaId",
                table: "Subjects",
                column: "SubjectAreaId",
                principalTable: "SubjectAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_SubjectAreas_SubjectAreaId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SubjectAreaId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SubjectAreaId",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "SubjectAreas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAreas_SubjectId",
                table: "SubjectAreas",
                column: "SubjectId");

            

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAreas_Subjects_SubjectId",
                table: "SubjectAreas",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
