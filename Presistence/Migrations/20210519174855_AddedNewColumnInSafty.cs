using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class AddedNewColumnInSafty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BubbleId",
                table: "bubbleSafetyDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
