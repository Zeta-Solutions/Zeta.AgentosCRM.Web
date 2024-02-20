using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_CRMInquiry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Inputtype",
                table: "LeadDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);

            migrationBuilder.CreateTable(
                name: "CRMInquiries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPreference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisaType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisaExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreferedInTake = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Institution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AcademicScore = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsGpa = table.Column<bool>(type: "bit", nullable: false),
                    Toefl = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Ielts = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Pte = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Sat1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Sat2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Gre = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GMat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PictureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    PassportCountryId = table.Column<int>(type: "int", nullable: true),
                    DegreeLevelId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    SubjectAreaId = table.Column<int>(type: "int", nullable: true),
                    OrganizationUnitId = table.Column<long>(type: "bigint", nullable: true),
                    LeadSourceId = table.Column<int>(type: "int", nullable: true),
                    TagId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_CRMInquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CRMInquiries_AbpOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CRMInquiries_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CRMInquiries_Countries_PassportCountryId",
                        column: x => x.PassportCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CRMInquiries_DegreeLevels_DegreeLevelId",
                        column: x => x.DegreeLevelId,
                        principalTable: "DegreeLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CRMInquiries_LeadSources_LeadSourceId",
                        column: x => x.LeadSourceId,
                        principalTable: "LeadSources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CRMInquiries_SubjectAreas_SubjectAreaId",
                        column: x => x.SubjectAreaId,
                        principalTable: "SubjectAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CRMInquiries_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CRMInquiries_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CRMInquiries_CountryId",
                table: "CRMInquiries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMInquiries_DegreeLevelId",
                table: "CRMInquiries",
                column: "DegreeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMInquiries_LeadSourceId",
                table: "CRMInquiries",
                column: "LeadSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMInquiries_OrganizationUnitId",
                table: "CRMInquiries",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMInquiries_PassportCountryId",
                table: "CRMInquiries",
                column: "PassportCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMInquiries_SubjectAreaId",
                table: "CRMInquiries",
                column: "SubjectAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMInquiries_SubjectId",
                table: "CRMInquiries",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMInquiries_TagId",
                table: "CRMInquiries",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMInquiries_TenantId",
                table: "CRMInquiries",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CRMInquiries");

            migrationBuilder.AlterColumn<string>(
                name: "Inputtype",
                table: "LeadDetails",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
