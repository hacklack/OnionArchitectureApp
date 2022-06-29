using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class CreatedBubbleMeetMembersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bubbleMeetDetails_bubbleDetails_BubbleId",
                table: "bubbleMeetDetails");

            migrationBuilder.DropIndex(
                name: "IX_bubbleMeetDetails_BubbleId",
                table: "bubbleMeetDetails");

            migrationBuilder.DropColumn(
                name: "BubbleId",
                table: "bubbleMeetDetails");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "bubbleMeetDetails",
                newName: "County");

            migrationBuilder.RenameColumn(
                name: "AllowChat",
                table: "bubbleMeetDetails",
                newName: "IsChatAllowed");

            migrationBuilder.AddColumn<DateTime>(
                name: "MeetDate",
                table: "bubbleMeetDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetDate",
                table: "bubbleMeetDetails");

            migrationBuilder.RenameColumn(
                name: "IsChatAllowed",
                table: "bubbleMeetDetails",
                newName: "AllowChat");

            migrationBuilder.RenameColumn(
                name: "County",
                table: "bubbleMeetDetails",
                newName: "ZipCode");

            migrationBuilder.AddColumn<int>(
                name: "BubbleId",
                table: "bubbleMeetDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_bubbleMeetDetails_BubbleId",
                table: "bubbleMeetDetails",
                column: "BubbleId");

            migrationBuilder.AddForeignKey(
                name: "FK_bubbleMeetDetails_bubbleDetails_BubbleId",
                table: "bubbleMeetDetails",
                column: "BubbleId",
                principalTable: "bubbleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
