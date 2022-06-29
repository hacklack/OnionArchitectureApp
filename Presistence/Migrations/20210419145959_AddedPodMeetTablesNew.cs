using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class AddedPodMeetTablesNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "podMeetDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetTiming = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsChatAllowed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_podMeetDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "podMeetMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PODMeetId = table.Column<int>(type: "int", nullable: false),
                    BubbleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PODId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_podMeetMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_podMeetMembers_bubbleDetails_BubbleId",
                        column: x => x.BubbleId,
                        principalTable: "bubbleDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_podMeetMembers_podDetails_PODId",
                        column: x => x.PODId,
                        principalTable: "podDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_podMeetMembers_podMeetDetails_PODMeetId",
                        column: x => x.PODMeetId,
                        principalTable: "podMeetDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_podMeetMembers_userDetails_UserId",
                        column: x => x.UserId,
                        principalTable: "userDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_podMeetMembers_BubbleId",
                table: "podMeetMembers",
                column: "BubbleId");

            migrationBuilder.CreateIndex(
                name: "IX_podMeetMembers_PODId",
                table: "podMeetMembers",
                column: "PODId");

            migrationBuilder.CreateIndex(
                name: "IX_podMeetMembers_PODMeetId",
                table: "podMeetMembers",
                column: "PODMeetId");

            migrationBuilder.CreateIndex(
                name: "IX_podMeetMembers_UserId",
                table: "podMeetMembers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "podMeetMembers");

            migrationBuilder.DropTable(
                name: "podMeetDetails");
        }
    }
}
