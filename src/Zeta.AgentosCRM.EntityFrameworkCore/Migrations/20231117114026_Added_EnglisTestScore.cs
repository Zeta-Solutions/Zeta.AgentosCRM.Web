using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_EnglisTestScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkflowId",
                table: "ClientInterstedServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EnglisTestScores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Listenting = table.Column<double>(type: "float", nullable: false),
                    Reading = table.Column<double>(type: "float", nullable: false),
                    Writing = table.Column<double>(type: "float", nullable: false),
                    Speaking = table.Column<double>(type: "float", nullable: false),
                    TotalScore = table.Column<double>(type: "float", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EnglisTestScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnglisTestScores_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientInterstedServices_WorkflowId",
                table: "ClientInterstedServices",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_EnglisTestScores_ClientId",
                table: "EnglisTestScores",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_EnglisTestScores_TenantId",
                table: "EnglisTestScores",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInterstedServices_Workflows_WorkflowId",
                table: "ClientInterstedServices",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientInterstedServices_Workflows_WorkflowId",
                table: "ClientInterstedServices");

            migrationBuilder.DropTable(
                name: "EnglisTestScores");

            migrationBuilder.DropIndex(
                name: "IX_ClientInterstedServices_WorkflowId",
                table: "ClientInterstedServices");

            migrationBuilder.DropColumn(
                name: "WorkflowId",
                table: "ClientInterstedServices");
        }
    }
}
