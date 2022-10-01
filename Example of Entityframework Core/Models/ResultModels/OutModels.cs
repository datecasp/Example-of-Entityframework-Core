using Example_of_Entityframework_Core.Models.DataModels;
using System.ComponentModel.DataAnnotations;

namespace Example_of_Entityframework_Core.Models.ResultModels
{
    public class LibrosPorCategoria
    {
        public int CategoriaId { get; set; }
        public string Categoria { get; set; }
        public ICollection<LibroBasico> Libros { get; set; }
    }

    public class LibroBasico
    {
        public int LibroId { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
    }

    public class CategoriasDeLibro
    {
        public int LibroId { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public ICollection<CategoriaBasica> Categorias { get; set; }
    }

    public class CategoriaBasica
    {
        public int CategoriaId { get; set; }
        public string Categoria { get; set; }
    }

    public class UsuarioBasico
    { 
        [Key]
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
    }
}
