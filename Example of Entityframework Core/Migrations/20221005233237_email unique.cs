using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example_of_Entityframework_Core.Migrations
{
    public partial class emailunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "GrantedUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "GrantedUsers",
                keyColumn: "GrantedUserId",
                keyValue: 1,
                column: "Email",
                value: "admin@admin.es");

            migrationBuilder.CreateIndex(
                name: "IX_GrantedUsers_Email",
                table: "GrantedUsers",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GrantedUsers_Email",
                table: "GrantedUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "GrantedUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "GrantedUsers",
                keyColumn: "GrantedUserId",
                keyValue: 1,
                column: "Email",
                value: "gonzalo@prueba.es");
        }
    }
}
