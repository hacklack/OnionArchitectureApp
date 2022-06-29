using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class AddedChatTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chatDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatTypeId = table.Column<int>(type: "int", nullable: false),
                    ChatParentTypeId = table.Column<int>(type: "int", nullable: false),
                    ChatParentId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chatHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    ChatMessageSenderId = table.Column<int>(type: "int", nullable: false),
                    ChatMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chatHistory_chatDetails_ChatId",
                        column: x => x.ChatId,
                        principalTable: "chatDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chatHistory_userDetails_ChatMessageSenderId",
                        column: x => x.ChatMessageSenderId,
                        principalTable: "userDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chatMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    ChatMemberId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chatMembers_chatDetails_ChatId",
                        column: x => x.ChatId,
                        principalTable: "chatDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chatMembers_userDetails_ChatMemberId",
                        column: x => x.ChatMemberId,
                        principalTable: "userDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chatHistory_ChatId",
                table: "chatHistory",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_chatHistory_ChatMessageSenderId",
                table: "chatHistory",
                column: "ChatMessageSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_chatMembers_ChatId",
                table: "chatMembers",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_chatMembers_ChatMemberId",
                table: "chatMembers",
                column: "ChatMemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chatHistory");

            migrationBuilder.DropTable(
                name: "chatMembers");

            migrationBuilder.DropTable(
                name: "chatDetails");
        }
    }
}
