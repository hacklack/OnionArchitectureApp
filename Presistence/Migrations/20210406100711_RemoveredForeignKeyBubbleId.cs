using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class RemoveredForeignKeyBubbleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bubbleMeetMembers_bubbleDetails_BubbleId",
                table: "bubbleMeetMembers");

            migrationBuilder.DropIndex(
                name: "IX_bubbleMeetMembers_BubbleId",
                table: "bubbleMeetMembers");

            migrationBuilder.DropColumn(
                name: "BubbleId",
                table: "bubbleMeetMembers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BubbleId",
                table: "bubbleMeetMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_bubbleMeetMembers_BubbleId",
                table: "bubbleMeetMembers",
                column: "BubbleId");

            migrationBuilder.AddForeignKey(
                name: "FK_bubbleMeetMembers_bubbleDetails_BubbleId",
                table: "bubbleMeetMembers",
                column: "BubbleId",
                principalTable: "bubbleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
