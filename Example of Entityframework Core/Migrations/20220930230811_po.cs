using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example_of_Entityframework_Core.Migrations
{
    public partial class po : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_UsuariosAntiguos_UsuariosAntiguosId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_UsuariosAntiguosId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuariosAntiguosId",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioAntiguoUsuarioId",
                table: "UsuariosAntiguos",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosAntiguos_UsuarioAntiguoUsuarioId",
                table: "UsuariosAntiguos",
                column: "UsuarioAntiguoUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosAntiguos_UsuarioBasico_UsuarioAntiguoUsuarioId",
                table: "UsuariosAntiguos",
                column: "UsuarioAntiguoUsuarioId",
                principalTable: "UsuarioBasico",
                principalColumn: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosAntiguos_UsuarioBasico_UsuarioAntiguoUsuarioId",
                table: "UsuariosAntiguos");

            migrationBuilder.DropTable(
                name: "UsuarioBasico");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosAntiguos_UsuarioAntiguoUsuarioId",
                table: "UsuariosAntiguos");

            migrationBuilder.DropColumn(
                name: "UsuarioAntiguoUsuarioId",
                table: "UsuariosAntiguos");

            migrationBuilder.AddColumn<int>(
                name: "UsuariosAntiguosId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UsuariosAntiguosId",
                table: "Usuarios",
                column: "UsuariosAntiguosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_UsuariosAntiguos_UsuariosAntiguosId",
                table: "Usuarios",
                column: "UsuariosAntiguosId",
                principalTable: "UsuariosAntiguos",
                principalColumn: "UsuariosAntiguosId");
        }
    }
}
