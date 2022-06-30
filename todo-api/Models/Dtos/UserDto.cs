using System.ComponentModel.DataAnnotations;

namespace todo_api.Models.Dtos
{
    public class UserDto
    {
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
