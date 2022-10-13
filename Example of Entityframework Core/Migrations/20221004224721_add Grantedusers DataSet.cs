using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example_of_Entityframework_Core.Migrations
{
    public partial class addGrantedusersDataSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GrantedUsers",
                columns: new[] { "GrantedUserId", "Email", "LastName", "Name", "Password" },
                values: new object[] { 1, "gonzalo@prueba.es", "de la predera", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "GrantedUsers",
                columns: new[] { "GrantedUserId", "Email", "LastName", "Name", "Password" },
                values: new object[] { 2, "pepe@prueba.es", "Lolailo", "User 1", "pepe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GrantedUsers",
                keyColumn: "GrantedUserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GrantedUsers",
                keyColumn: "GrantedUserId",
                keyValue: 2);
        }
    }
}
