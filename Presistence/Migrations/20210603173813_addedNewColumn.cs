using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class addedNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bubbleSafetyDetails_bubbleDetails_BubbleId",
                table: "bubbleSafetyDetails");

            migrationBuilder.AlterColumn<double>(
                name: "BubbleSaftyValue",
                table: "bubbleSafetyDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BubbleId",
                table: "bubbleSafetyDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BubblePODId",
                table: "bubbleSafetyDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_bubbleSafetyDetails_bubbleDetails_BubbleId",
                table: "bubbleSafetyDetails",
                column: "BubbleId",
                principalTable: "bubbleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bubbleSafetyDetails_bubbleDetails_BubbleId",
                table: "bubbleSafetyDetails");

            migrationBuilder.DropColumn(
                name: "BubblePODId",
                table: "bubbleSafetyDetails");

            migrationBuilder.AlterColumn<string>(
                name: "BubbleSaftyValue",
                table: "bubbleSafetyDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "BubbleId",
                table: "bubbleSafetyDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_bubbleSafetyDetails_bubbleDetails_BubbleId",
                table: "bubbleSafetyDetails",
                column: "BubbleId",
                principalTable: "bubbleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
