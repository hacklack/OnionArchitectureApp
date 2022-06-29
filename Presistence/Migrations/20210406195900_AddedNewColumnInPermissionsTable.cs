using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class AddedNewColumnInPermissionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserPermission",
                table: "bubbleMeetMemberPermissions",
                newName: "UserPermissionTypeId");

            migrationBuilder.AddColumn<bool>(
                name: "UserPermissionStatus",
                table: "bubbleMeetMemberPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPermissionStatus",
                table: "bubbleMeetMemberPermissions");

            migrationBuilder.RenameColumn(
                name: "UserPermissionTypeId",
                table: "bubbleMeetMemberPermissions",
                newName: "UserPermission");
        }
    }
}
