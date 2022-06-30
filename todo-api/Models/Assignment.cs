namespace todo_api.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public StatusType Status { get; set; }

        public enum StatusType
        {
            Done,
            InProgress
        }
    }
}
