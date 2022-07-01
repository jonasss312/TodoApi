using todo_api.Models;

namespace todo_api.Usecases.Interfaces
{
    public interface GetUserAssignmentUC
    {
        Task<Assignment> GetUserAssignment(int userId, int assignmentId);
    }
}
