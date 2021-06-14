using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.Migrations
{
    public partial class add_ExtendedUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_appUsers_appUserId",
                table: "photos");

            migrationBuilder.AlterColumn<int>(
                name: "appUserId",
                table: "photos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_photos_appUsers_appUserId",
                table: "photos",
                column: "appUserId",
                principalTable: "appUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_appUsers_appUserId",
                table: "photos");

            migrationBuilder.AlterColumn<int>(
                name: "appUserId",
                table: "photos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_appUsers_appUserId",
                table: "photos",
                column: "appUserId",
                principalTable: "appUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
