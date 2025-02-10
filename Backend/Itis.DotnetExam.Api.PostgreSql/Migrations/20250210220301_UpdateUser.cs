using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itis.DotnetExam.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "public",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "public",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "public",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
