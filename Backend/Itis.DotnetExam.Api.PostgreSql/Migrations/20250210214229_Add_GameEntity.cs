using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itis.DotnetExam.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class Add_GameEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OpponentGameId",
                schema: "public",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerGameId",
                schema: "public",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "games",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Id хоста"),
                    OpponentId = table.Column<Guid>(type: "uuid", nullable: true, comment: "Id оппонента"),
                    MaxRate = table.Column<int>(type: "integer", nullable: false, comment: "Максимальный рейтинг"),
                    GameState = table.Column<int>(type: "integer", nullable: false, comment: "Статус игры"),
                    GameMap = table.Column<int[]>(type: "integer[]", nullable: false, comment: "Игровая карта")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_games_users_OpponentId",
                        column: x => x.OpponentId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_games_users_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Игровое лобби");

            migrationBuilder.CreateIndex(
                name: "IX_games_OpponentId",
                schema: "public",
                table: "games",
                column: "OpponentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_games_OwnerId",
                schema: "public",
                table: "games",
                column: "OwnerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "games",
                schema: "public");

            migrationBuilder.DropColumn(
                name: "OpponentGameId",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "OwnerGameId",
                schema: "public",
                table: "users");
        }
    }
}
