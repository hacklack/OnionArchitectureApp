using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class AddedNewColumnsInNotificationHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PODBubbleChildId",
                table: "notificationsHistory",
                newName: "UserDeviceId");

            migrationBuilder.AddColumn<int>(
                name: "PODBubbleId",
                table: "notificationsHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NotificationImage",
                table: "notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_notificationsHistory_NotificationId",
                table: "notificationsHistory",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_notificationsHistory_UserId",
                table: "notificationsHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_notificationsHistory_notifications_NotificationId",
                table: "notificationsHistory",
                column: "NotificationId",
                principalTable: "notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notificationsHistory_userDetails_UserId",
                table: "notificationsHistory",
                column: "UserId",
                principalTable: "userDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notificationsHistory_notifications_NotificationId",
                table: "notificationsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_notificationsHistory_userDetails_UserId",
                table: "notificationsHistory");

            migrationBuilder.DropIndex(
                name: "IX_notificationsHistory_NotificationId",
                table: "notificationsHistory");

            migrationBuilder.DropIndex(
                name: "IX_notificationsHistory_UserId",
                table: "notificationsHistory");

            migrationBuilder.DropColumn(
                name: "PODBubbleId",
                table: "notificationsHistory");

            migrationBuilder.DropColumn(
                name: "NotificationImage",
                table: "notifications");

            migrationBuilder.RenameColumn(
                name: "UserDeviceId",
                table: "notificationsHistory",
                newName: "PODBubbleChildId");
        }
    }
}
