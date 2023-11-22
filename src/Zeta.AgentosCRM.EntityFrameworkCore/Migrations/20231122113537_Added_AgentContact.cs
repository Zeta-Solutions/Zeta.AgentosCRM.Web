using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_AgentContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AgentId",
                table: "Notes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubAgent",
                table: "Agents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgentContacts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    AgentId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_AgentContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgentContacts_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_AgentId",
                table: "Notes",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentContacts_AgentId",
                table: "AgentContacts",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentContacts_TenantId",
                table: "AgentContacts",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Agents_AgentId",
                table: "Notes",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Agents_AgentId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "AgentContacts");

            migrationBuilder.DropIndex(
                name: "IX_Notes_AgentId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IsSubAgent",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Agents");
        }
    }
}
