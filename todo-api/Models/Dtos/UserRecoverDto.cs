using System.ComponentModel.DataAnnotations;

namespace todo_api.Models.Dtos
{
    public class UserRecoverDto
    {
        [Required]
        public string Email { get; set; } = String.Empty;
    }
}
