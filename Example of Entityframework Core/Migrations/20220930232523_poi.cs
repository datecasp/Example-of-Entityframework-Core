using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example_of_Entityframework_Core.Migrations
{
    public partial class poi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LibroId",
                table: "UsuariosAntiguos",
                newName: "LibroAntiguoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LibroAntiguoId",
                table: "UsuariosAntiguos",
                newName: "LibroId");
        }
    }
}
