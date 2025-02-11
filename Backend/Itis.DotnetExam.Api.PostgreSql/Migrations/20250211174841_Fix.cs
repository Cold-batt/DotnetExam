using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itis.DotnetExam.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "public",
                table: "games",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "public",
                table: "games",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)",
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
