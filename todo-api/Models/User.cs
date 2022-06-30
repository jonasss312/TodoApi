using System.ComponentModel.DataAnnotations;

namespace todo_api.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
    }
}
