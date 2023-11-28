using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_DocumentCheckListProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentCheckListProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_DocumentCheckListProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentCheckListProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentCheckListProducts_WorkflowStepDocumentCheckLists_WorkflowStepDocumentCheckListId",
                        column: x => x.WorkflowStepDocumentCheckListId,
                        principalTable: "WorkflowStepDocumentCheckLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCheckListProducts_ProductId",
                table: "DocumentCheckListProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCheckListProducts_TenantId",
                table: "DocumentCheckListProducts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCheckListProducts_WorkflowStepDocumentCheckListId",
                table: "DocumentCheckListProducts",
                column: "WorkflowStepDocumentCheckListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentCheckListProducts");
        }
    }
}
