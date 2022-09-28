using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Example_of_Entityframework_Core.Models.DataModels
{
    public class Categorias
    {
        [Key]
        [Required]
        public int CategoriasId { get; set; }
        [Required]
        public string Categoria { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ICollection<CategoriaLibro> CategoriaLibros { get; set; }
    }
}
