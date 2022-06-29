using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class removedFKConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bubbleMeetMemberPermissions_bubbleMeetDetails_BubbleMeetId",
                table: "bubbleMeetMemberPermissions");

            migrationBuilder.DropIndex(
                name: "IX_bubbleMeetMemberPermissions_BubbleMeetId",
                table: "bubbleMeetMemberPermissions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_bubbleMeetMemberPermissions_BubbleMeetId",
                table: "bubbleMeetMemberPermissions",
                column: "BubbleMeetId");

            migrationBuilder.AddForeignKey(
                name: "FK_bubbleMeetMemberPermissions_bubbleMeetDetails_BubbleMeetId",
                table: "bubbleMeetMemberPermissions",
                column: "BubbleMeetId",
                principalTable: "bubbleMeetDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
