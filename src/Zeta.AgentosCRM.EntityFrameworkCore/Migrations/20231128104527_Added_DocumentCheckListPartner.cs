using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_DocumentCheckListPartner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentCheckListPartners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerId = table.Column<long>(type: "bigint", nullable: false),
                    WorkflowStepDocumentCheckListId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_DocumentCheckListPartners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentCheckListPartners_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentCheckListPartners_WorkflowStepDocumentCheckLists_WorkflowStepDocumentCheckListId",
                        column: x => x.WorkflowStepDocumentCheckListId,
                        principalTable: "WorkflowStepDocumentCheckLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCheckListPartners_PartnerId",
                table: "DocumentCheckListPartners",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCheckListPartners_TenantId",
                table: "DocumentCheckListPartners",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCheckListPartners_WorkflowStepDocumentCheckListId",
                table: "DocumentCheckListPartners",
                column: "WorkflowStepDocumentCheckListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentCheckListPartners");
        }
    }
}
