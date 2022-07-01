using System.ComponentModel.DataAnnotations;
using static todo_api.Models.Assignment;

namespace todo_api.Models.Dtos
{
    public class AssignmentDto
    {
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public StatusType Status { get; set; }
    }
}
