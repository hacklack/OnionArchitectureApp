using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class AddedThreeNewColumnBubbleSafetyCalculationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CasesToPopulationRatio",
                table: "bubbleSafetyWightsCalculationFields",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DeathToPopulationRatio",
                table: "bubbleSafetyWightsCalculationFields",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VaccineToPopulationRatio",
                table: "bubbleSafetyWightsCalculationFields",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CasesToPopulationRatio",
                table: "bubbleSafetyWightsCalculationFields");

            migrationBuilder.DropColumn(
                name: "DeathToPopulationRatio",
                table: "bubbleSafetyWightsCalculationFields");

            migrationBuilder.DropColumn(
                name: "VaccineToPopulationRatio",
                table: "bubbleSafetyWightsCalculationFields");
        }
    }
}
