using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Example_of_Entityframework_Core.Models.DataModels
{
    public class CategoriaLibro
    {
        [JsonIgnore]
        [Key]
        [Required]
        public int CategoriaLibroId { get; set; }
        [JsonIgnore]
        [Required]
        public int CategoriaId { get; set; }
        public Categorias Categoria { get; set; }
        [JsonIgnore]
        [Required]
        public int LibroId { get; set; }
        public Libro Libro { get; set; }
    }
}
