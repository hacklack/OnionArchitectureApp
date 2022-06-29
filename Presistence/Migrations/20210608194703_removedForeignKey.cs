using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class removedForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bubbleSafetyDetails_bubbleDetails_BubbleId",
                table: "bubbleSafetyDetails");

            migrationBuilder.DropIndex(
                name: "IX_bubbleSafetyDetails_BubbleId",
                table: "bubbleSafetyDetails");

            migrationBuilder.DropColumn(
                name: "BubbleId",
                table: "bubbleSafetyDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BubbleId",
                table: "bubbleSafetyDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_bubbleSafetyDetails_BubbleId",
                table: "bubbleSafetyDetails",
                column: "BubbleId");

            migrationBuilder.AddForeignKey(
                name: "FK_bubbleSafetyDetails_bubbleDetails_BubbleId",
                table: "bubbleSafetyDetails",
                column: "BubbleId",
                principalTable: "bubbleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
