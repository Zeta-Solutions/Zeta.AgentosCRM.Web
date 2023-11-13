using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_ClientEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientEducations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    DegreeTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CourseStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcdamicScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubjectAreaId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientEducations_SubjectAreas_SubjectAreaId",
                        column: x => x.SubjectAreaId,
                        principalTable: "SubjectAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientEducations_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientEducations_SubjectAreaId",
                table: "ClientEducations",
                column: "SubjectAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEducations_SubjectId",
                table: "ClientEducations",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEducations_TenantId",
                table: "ClientEducations",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientEducations");
        }
    }
}
