using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class removedFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_podBubbleMembers_bubbleMembers_PODBubbleMemberId",
                table: "podBubbleMembers");

            migrationBuilder.DropIndex(
                name: "IX_podBubbleMembers_PODBubbleMemberId",
                table: "podBubbleMembers");

            migrationBuilder.DropColumn(
                name: "PODBubbleMemberId",
                table: "podBubbleMembers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PODBubbleMemberId",
                table: "podBubbleMembers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_podBubbleMembers_PODBubbleMemberId",
                table: "podBubbleMembers",
                column: "PODBubbleMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_podBubbleMembers_bubbleMembers_PODBubbleMemberId",
                table: "podBubbleMembers",
                column: "PODBubbleMemberId",
                principalTable: "bubbleMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
