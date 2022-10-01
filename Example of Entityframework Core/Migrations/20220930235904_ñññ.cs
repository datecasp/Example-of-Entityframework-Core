using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example_of_Entityframework_Core.Migrations
{
    public partial class ñññ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosAntiguos_UsuarioBasico_UsuarioAntiguoUsuarioId",
                table: "UsuariosAntiguos");

            migrationBuilder.DropTable(
                name: "UsuarioBasico");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosAntiguos_Usuarios_UsuarioAntiguoUsuarioId",
                table: "UsuariosAntiguos",
                column: "UsuarioAntiguoUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosAntiguos_Usuarios_UsuarioAntiguoUsuarioId",
                table: "UsuariosAntiguos");

            migrationBuilder.CreateTable(
                name: "UsuarioBasico",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioBasico", x => x.UsuarioId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosAntiguos_UsuarioBasico_UsuarioAntiguoUsuarioId",
                table: "UsuariosAntiguos",
                column: "UsuarioAntiguoUsuarioId",
                principalTable: "UsuarioBasico",
                principalColumn: "UsuarioId");
        }
    }
}
