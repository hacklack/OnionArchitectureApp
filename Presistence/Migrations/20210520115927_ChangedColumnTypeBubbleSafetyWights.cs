using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class ChangedColumnTypeBubbleSafetyWights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BubbleWightFiled",
                table: "bubbleSafetyWights");

            migrationBuilder.AddColumn<int>(
                name: "BubbleWightFiledTypeId",
                table: "bubbleSafetyWights",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BubbleWightFiledTypeId",
                table: "bubbleSafetyWights");

            migrationBuilder.AddColumn<string>(
                name: "BubbleWightFiled",
                table: "bubbleSafetyWights",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
