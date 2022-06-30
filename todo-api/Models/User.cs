using System.ComponentModel.DataAnnotations;

namespace todo_api.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
        [Required]
        public RoleType Role { get; set; }

        public enum RoleType
        {
            Admin,
            User
        }
    }
}
