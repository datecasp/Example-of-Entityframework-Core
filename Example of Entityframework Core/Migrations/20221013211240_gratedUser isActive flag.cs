using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example_of_Entityframework_Core.Migrations
{
    public partial class gratedUserisActiveflag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "GrantedUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "GrantedUsers",
                keyColumn: "GrantedUserId",
                keyValue: 1,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "GrantedUsers",
                keyColumn: "GrantedUserId",
                keyValue: 2,
                column: "isActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "GrantedUsers");
        }
    }
}
