using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class AddedNewTableBubbleSafetyCalculationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bubbleSafetyWightsCalculationFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Population = table.Column<double>(type: "float", nullable: false),
                    TestPositivityRatio = table.Column<double>(type: "float", nullable: false),
                    CaseDensity = table.Column<double>(type: "float", nullable: false),
                    InfectionRate = table.Column<double>(type: "float", nullable: false),
                    InfectionRateCI90 = table.Column<double>(type: "float", nullable: false),
                    ActualCases = table.Column<double>(type: "float", nullable: false),
                    ActualDeaths = table.Column<double>(type: "float", nullable: false),
                    ActualVaccineCompleted = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bubbleSafetyWightsCalculationFields", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bubbleSafetyWightsCalculationFields");
        }
    }
}
