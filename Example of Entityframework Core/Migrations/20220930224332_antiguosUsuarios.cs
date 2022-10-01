using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example_of_Entityframework_Core.Migrations
{
    public partial class antiguosUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuariosAntiguosId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsuariosAntiguos",
                columns: table => new
                {
                    UsuariosAntiguosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibroId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosAntiguos", x => x.UsuariosAntiguosId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_UsuariosAntiguos_UsuariosAntiguosId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "UsuariosAntiguos");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_UsuariosAntiguosId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuariosAntiguosId",
                table: "Usuarios");
        }
    }
}
