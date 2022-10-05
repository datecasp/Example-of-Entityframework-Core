using System.ComponentModel.DataAnnotations;

namespace Example_of_Entityframework_Core.Models.DataModels
{
    public class UserLogins
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
