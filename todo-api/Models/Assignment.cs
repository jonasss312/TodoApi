using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todo_api.Models
{
    public class Assignment
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public StatusType Status { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public enum StatusType
        {
            Done,
            InProgress
        }
    }
}
