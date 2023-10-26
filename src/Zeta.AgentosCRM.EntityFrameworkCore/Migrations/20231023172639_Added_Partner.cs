using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_Partner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    PartnerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketingEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePictureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasterCategoryId = table.Column<int>(type: "int", nullable: false),
                    PartnerTypeId = table.Column<int>(type: "int", nullable: false),
                    WorkflowId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryCodeId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Partners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partners_AppBinaryObjects_ProfilePictureId",
                        column: x => x.ProfilePictureId,
                        principalTable: "AppBinaryObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partners_Countries_CountryCodeId",
                        column: x => x.CountryCodeId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Partners_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partners_MasterCategories_MasterCategoryId",
                        column: x => x.MasterCategoryId,
                        principalTable: "MasterCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partners_PartnerTypes_PartnerTypeId",
                        column: x => x.PartnerTypeId,
                        principalTable: "PartnerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Partners_Workflows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partners_CountryCodeId",
                table: "Partners",
                column: "CountryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_CountryId",
                table: "Partners",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_MasterCategoryId",
                table: "Partners",
                column: "MasterCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_PartnerTypeId",
                table: "Partners",
                column: "PartnerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_ProfilePictureId",
                table: "Partners",
                column: "ProfilePictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_TenantId",
                table: "Partners",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_WorkflowId",
                table: "Partners",
                column: "WorkflowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partners");
        }
    }
}
