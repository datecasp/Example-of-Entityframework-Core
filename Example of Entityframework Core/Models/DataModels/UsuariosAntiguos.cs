using Example_of_Entityframework_Core.Models.ResultModels;
using System.ComponentModel.DataAnnotations;

namespace Example_of_Entityframework_Core.Models.DataModels
{
    public class UsuariosAntiguos
    {
        [Key]
        [Required]
        public int UsuariosAntiguosId { get; set; }

        public int? LibroAntiguoId { get; set; }

        public Usuario? UsuarioAntiguo { get; set; }


    }
}
