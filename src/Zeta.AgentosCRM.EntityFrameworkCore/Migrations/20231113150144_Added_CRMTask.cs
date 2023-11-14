using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_CRMTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CRMTasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Attachment = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RelatedTo = table.Column<int>(type: "int", nullable: false),
                    InternalId = table.Column<int>(type: "int", nullable: false),
                    TaskCategoryId = table.Column<int>(type: "int", nullable: false),
                    AssigneeId = table.Column<long>(type: "bigint", nullable: false),
                    TaskPriorityId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: true),
                    PartnerId = table.Column<long>(type: "bigint", nullable: true),
                    ApplicationId = table.Column<long>(type: "bigint", nullable: true),
                    ApplicationStageId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_CRMTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CRMTasks_AbpUsers_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CRMTasks_ApplicationStages_ApplicationStageId",
                        column: x => x.ApplicationStageId,
                        principalTable: "ApplicationStages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CRMTasks_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CRMTasks_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CRMTasks_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CRMTasks_TaskCategories_TaskCategoryId",
                        column: x => x.TaskCategoryId,
                        principalTable: "TaskCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CRMTasks_TaskPriorities_TaskPriorityId",
                        column: x => x.TaskPriorityId,
                        principalTable: "TaskPriorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CRMTasks_ApplicationId",
                table: "CRMTasks",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMTasks_ApplicationStageId",
                table: "CRMTasks",
                column: "ApplicationStageId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMTasks_AssigneeId",
                table: "CRMTasks",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMTasks_ClientId",
                table: "CRMTasks",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMTasks_PartnerId",
                table: "CRMTasks",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMTasks_TaskCategoryId",
                table: "CRMTasks",
                column: "TaskCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMTasks_TaskPriorityId",
                table: "CRMTasks",
                column: "TaskPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMTasks_TenantId",
                table: "CRMTasks",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CRMTasks");
        }
    }
}
