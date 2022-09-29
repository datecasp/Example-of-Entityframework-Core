using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example_of_Entityframework_Core.Migrations
{
    public partial class full : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Usuarios_UsuarioId",
                table: "Libros");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Libros",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Usuarios_UsuarioId",
                table: "Libros",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Usuarios_UsuarioId",
                table: "Libros");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Libros",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Usuarios_UsuarioId",
                table: "Libros",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
