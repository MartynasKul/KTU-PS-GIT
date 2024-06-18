using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portal_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSponsorsToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date",
                schema: "dbo",
                table: "Comment",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.CreateTable(
                name: "ProjectSponsor",
                schema: "dbo",
                columns: table => new
                {
                    ProjectsId = table.Column<int>(type: "int", nullable: false),
                    SponsorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSponsor", x => new { x.ProjectsId, x.SponsorsId });
                    table.ForeignKey(
                        name: "FK_ProjectSponsor_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalSchema: "dbo",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSponsor_Sponsor_SponsorsId",
                        column: x => x.SponsorsId,
                        principalSchema: "dbo",
                        principalTable: "Sponsor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSponsor_SponsorsId",
                schema: "dbo",
                table: "ProjectSponsor",
                column: "SponsorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectSponsor",
                schema: "dbo");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date",
                schema: "dbo",
                table: "Comment",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);
        }
    }
}
