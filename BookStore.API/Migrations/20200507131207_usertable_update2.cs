using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.API.Migrations
{
    public partial class usertable_update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobLevel",
                table: "User",
                newName: "job_level");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "User",
                newName: "job_id");

            migrationBuilder.AlterColumn<int>(
                name: "job_level",
                table: "User",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "job_id",
                table: "User",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "job_level",
                table: "User",
                newName: "JobLevel");

            migrationBuilder.RenameColumn(
                name: "job_id",
                table: "User",
                newName: "JobId");

            migrationBuilder.AlterColumn<int>(
                name: "JobLevel",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValueSql: "((1))");
        }
    }
}
