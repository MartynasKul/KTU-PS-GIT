using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portal_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSmtpConfigurationInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SmtpServer",
                schema: "dbo",
                table: "Organization",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SmtpServerPort",
                schema: "dbo",
                table: "Organization",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmtpServerUserName",
                schema: "dbo",
                table: "Organization",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmtpServerUserPassword",
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
                name: "SmtpServer",
                schema: "dbo",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "SmtpServerPort",
                schema: "dbo",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "SmtpServerUserName",
                schema: "dbo",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "SmtpServerUserPassword",
                schema: "dbo",
                table: "Organization");
        }
    }
}
