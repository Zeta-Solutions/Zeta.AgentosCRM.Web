using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeta.AgentosCRM.Migrations
{
    /// <inheritdoc />
    public partial class Added_Client : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactPreferences = table.Column<int>(type: "int", nullable: false),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferedIntake = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassportNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisaType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisaExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    InternalId = table.Column<int>(type: "int", nullable: false),
                    ClientPortal = table.Column<bool>(type: "bit", nullable: false),
                    SecondaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Archived = table.Column<bool>(type: "bit", nullable: false),
                    CountryCodeId = table.Column<int>(type: "int", nullable: false),
                    AssigneeId = table.Column<long>(type: "bigint", nullable: false),
                    ProfilePictureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HighestQualificationId = table.Column<int>(type: "int", nullable: true),
                    StudyAreaId = table.Column<int>(type: "int", nullable: true),
                    LeadSourceId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    PassportCountryId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_AbpUsers_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_AppBinaryObjects_ProfilePictureId",
                        column: x => x.ProfilePictureId,
                        principalTable: "AppBinaryObjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clients_Countries_CountryCodeId",
                        column: x => x.CountryCodeId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clients_Countries_PassportCountryId",
                        column: x => x.PassportCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clients_DegreeLevels_HighestQualificationId",
                        column: x => x.HighestQualificationId,
                        principalTable: "DegreeLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clients_LeadSources_LeadSourceId",
                        column: x => x.LeadSourceId,
                        principalTable: "LeadSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_SubjectAreas_StudyAreaId",
                        column: x => x.StudyAreaId,
                        principalTable: "SubjectAreas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AssigneeId",
                table: "Clients",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CountryCodeId",
                table: "Clients",
                column: "CountryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CountryId",
                table: "Clients",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_HighestQualificationId",
                table: "Clients",
                column: "HighestQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_LeadSourceId",
                table: "Clients",
                column: "LeadSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PassportCountryId",
                table: "Clients",
                column: "PassportCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ProfilePictureId",
                table: "Clients",
                column: "ProfilePictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_StudyAreaId",
                table: "Clients",
                column: "StudyAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TenantId",
                table: "Clients",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
