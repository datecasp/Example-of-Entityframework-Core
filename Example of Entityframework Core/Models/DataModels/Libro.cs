using Example_of_Entityframework_Core.Models.ResultModels;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Example_of_Entityframework_Core.Models.DataModels
{
    public class Libro
    {
        [Key]
        [Required]
        public int LibroId { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Autor { get; set; }

        // 1:N con Usuario
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? UsuarioId { get; set; }
       
        //N:M con Categorías
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ICollection<CategoriaLibro> CategoriaLibros { get; set; }
    }
}
