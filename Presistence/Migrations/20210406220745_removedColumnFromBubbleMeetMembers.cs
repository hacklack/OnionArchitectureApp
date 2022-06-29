using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class removedColumnFromBubbleMeetMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBubbleMeetAdmin",
                table: "bubbleMeetMembers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBubbleMeetAdmin",
                table: "bubbleMeetMembers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
