using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itis.DotnetExam.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class Chat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChatId",
                schema: "public",
                table: "games",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "chats",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.Id);
                },
                comment: "Чаты");

            migrationBuilder.CreateTable(
                name: "messages",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    Text = table.Column<string>(type: "text", nullable: false, comment: "Текст сообщения"),
                    UserName = table.Column<string>(type: "text", nullable: false, comment: "Пользователь"),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_messages_chats_ChatId",
                        column: x => x.ChatId,
                        principalSchema: "public",
                        principalTable: "chats",
                        principalColumn: "Id");
                },
                comment: "Сообщения");

            migrationBuilder.CreateIndex(
                name: "IX_games_ChatId",
                schema: "public",
                table: "games",
                column: "ChatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_messages_ChatId",
                schema: "public",
                table: "messages",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_games_chats_ChatId",
                schema: "public",
                table: "games",
                column: "ChatId",
                principalSchema: "public",
                principalTable: "chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_games_chats_ChatId",
                schema: "public",
                table: "games");

            migrationBuilder.DropTable(
                name: "messages",
                schema: "public");

            migrationBuilder.DropTable(
                name: "chats",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_games_ChatId",
                schema: "public",
                table: "games");

            migrationBuilder.DropColumn(
                name: "ChatId",
                schema: "public",
                table: "games");
        }
    }
}
