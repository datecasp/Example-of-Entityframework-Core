using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Example_of_Entityframework_Core.Models.DataModels
{
    public class Usuario
    {
        [Key]
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ICollection<Libro> Libros { get; set; }
        
    }
}
