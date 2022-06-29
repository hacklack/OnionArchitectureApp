using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class changedcolumnname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notifications_notifications_NotificationTypeIdId",
                table: "notifications");

            migrationBuilder.DropIndex(
                name: "IX_notifications_NotificationTypeIdId",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "NotificationTypeIdId",
                table: "notifications");

            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeId",
                table: "notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationTypeId",
                table: "notifications");

            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeIdId",
                table: "notifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_notifications_NotificationTypeIdId",
                table: "notifications",
                column: "NotificationTypeIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_notifications_NotificationTypeIdId",
                table: "notifications",
                column: "NotificationTypeIdId",
                principalTable: "notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
