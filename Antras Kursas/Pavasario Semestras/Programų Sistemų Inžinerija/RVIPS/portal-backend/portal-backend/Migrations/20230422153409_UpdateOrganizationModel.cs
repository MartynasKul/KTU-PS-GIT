using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portal_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrganizationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImapServer",
                schema: "dbo",
                table: "Organization",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImapServerPort",
                schema: "dbo",
                table: "Organization",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImapServerUserName",
                schema: "dbo",
                table: "Organization",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImapServerUserPassword",
                schema: "dbo",
                table: "Organization",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImapServer",
                schema: "dbo",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "ImapServerPort",
                schema: "dbo",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "ImapServerUserName",
                schema: "dbo",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "ImapServerUserPassword",
                schema: "dbo",
                table: "Organization");
        }
    }
}
