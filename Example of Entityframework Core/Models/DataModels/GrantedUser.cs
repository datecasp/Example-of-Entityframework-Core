using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Example_of_Entityframework_Core.Models.DataModels
{
    [Index(nameof(Email), IsUnique = true)]
    public class GrantedUser
    {
        [Key]
        [Required]
        public int GrantedUserId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        public Role Role { get; set; }

        [Required]
        public bool isActive { get; set; } = true;
    }

    public enum Role
    {
        Admin,
        User
    }
}
