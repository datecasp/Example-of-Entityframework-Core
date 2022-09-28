using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example_of_Entityframework_Core.Migrations
{
    public partial class nam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriasId);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasLibro",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    LibroId = table.Column<int>(type: "int", nullable: false),
                    CategoriaLibroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasLibro", x => new { x.LibroId, x.CategoriaId });
                    table.ForeignKey(
                        name: "FK_CategoriasLibro_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriasId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriasLibro_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "LibroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriasId", "Categoria" },
                values: new object[,]
                {
                    { -4, "Cat4" },
                    { -3, "Cat3" },
                    { -2, "Cat2" },
                    { -1, "Cat1" }
                });

            migrationBuilder.InsertData(
                table: "CategoriasLibro",
                columns: new[] { "CategoriaId", "LibroId", "CategoriaLibroId" },
                values: new object[,]
                {
                    { -4, -6, -6 },
                    { -4, -5, -5 },
                    { -3, -5, -4 },
                    { -2, -4, -3 },
                    { -4, -3, -7 },
                    { -1, -2, -2 },
                    { -1, -1, -1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasLibro_CategoriaId",
                table: "CategoriasLibro",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriasLibro");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
