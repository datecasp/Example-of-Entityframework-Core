using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example_of_Entityframework_Core.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriasLibro_Categorias_CategoriaId",
                table: "CategoriasLibro");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriasLibro_Libros_LibroId",
                table: "CategoriasLibro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriasLibro",
                table: "CategoriasLibro");

            migrationBuilder.RenameTable(
                name: "CategoriasLibro",
                newName: "CategoriaLibros");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriasLibro_CategoriaId",
                table: "CategoriaLibros",
                newName: "IX_CategoriaLibros_CategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaLibros",
                table: "CategoriaLibros",
                columns: new[] { "LibroId", "CategoriaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaLibros_Categorias_CategoriaId",
                table: "CategoriaLibros",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriasId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaLibros_Libros_LibroId",
                table: "CategoriaLibros",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "LibroId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaLibros_Categorias_CategoriaId",
                table: "CategoriaLibros");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaLibros_Libros_LibroId",
                table: "CategoriaLibros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaLibros",
                table: "CategoriaLibros");

            migrationBuilder.RenameTable(
                name: "CategoriaLibros",
                newName: "CategoriasLibro");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriaLibros_CategoriaId",
                table: "CategoriasLibro",
                newName: "IX_CategoriasLibro_CategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriasLibro",
                table: "CategoriasLibro",
                columns: new[] { "LibroId", "CategoriaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriasLibro_Categorias_CategoriaId",
                table: "CategoriasLibro",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriasId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriasLibro_Libros_LibroId",
                table: "CategoriasLibro",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "LibroId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
