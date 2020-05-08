using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.API.Migrations
{
    public partial class usertable_update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__User__role_id__6E565CE8",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "User",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_role_id",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.AlterColumn<short>(
                name: "RoleId",
                table: "User",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldDefaultValueSql: "((1))");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "User",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "User",
                newName: "IX_User_role_id");

            migrationBuilder.AlterColumn<short>(
                name: "role_id",
                table: "User",
                type: "smallint",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(short),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__User__role_id__6E565CE8",
                table: "User",
                column: "role_id",
                principalTable: "Role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
