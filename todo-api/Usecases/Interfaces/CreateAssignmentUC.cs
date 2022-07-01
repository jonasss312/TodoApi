using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Usecases.Interfaces
{
    public interface CreateAssignmentUC
    {
        Task CreateAssignment(AssignmentDto assignmentDto, int userId, out Assignment assignment);
    }
}
