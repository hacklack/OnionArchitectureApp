using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class AddedUserPermissionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bubbleMeetMemberPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BubbleMeetId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserPermission = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bubbleMeetMemberPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bubbleMeetMemberPermissions_bubbleMeetDetails_BubbleMeetId",
                        column: x => x.BubbleMeetId,
                        principalTable: "bubbleMeetDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bubbleMeetMemberPermissions_userDetails_UserId",
                        column: x => x.UserId,
                        principalTable: "userDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bubbleMeetMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BubbleId = table.Column<int>(type: "int", nullable: false),
                    BubbleMeetId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bubbleMeetMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bubbleMeetMembers_bubbleDetails_BubbleId",
                        column: x => x.BubbleId,
                        principalTable: "bubbleDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bubbleMeetMembers_bubbleMeetDetails_BubbleMeetId",
                        column: x => x.BubbleMeetId,
                        principalTable: "bubbleMeetDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bubbleMeetMembers_userDetails_UserId",
                        column: x => x.UserId,
                        principalTable: "userDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bubbleMeetMemberPermissions_BubbleMeetId",
                table: "bubbleMeetMemberPermissions",
                column: "BubbleMeetId");

            migrationBuilder.CreateIndex(
                name: "IX_bubbleMeetMemberPermissions_UserId",
                table: "bubbleMeetMemberPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_bubbleMeetMembers_BubbleId",
                table: "bubbleMeetMembers",
                column: "BubbleId");

            migrationBuilder.CreateIndex(
                name: "IX_bubbleMeetMembers_BubbleMeetId",
                table: "bubbleMeetMembers",
                column: "BubbleMeetId");

            migrationBuilder.CreateIndex(
                name: "IX_bubbleMeetMembers_UserId",
                table: "bubbleMeetMembers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bubbleMeetMemberPermissions");

            migrationBuilder.DropTable(
                name: "bubbleMeetMembers");
        }
    }
}
