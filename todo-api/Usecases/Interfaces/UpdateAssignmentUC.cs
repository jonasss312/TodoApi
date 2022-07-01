using todo_api.Models;

namespace todo_api.Usecases.Interfaces
{
    public interface UpdateAssignmentUC
    {
        Task UpdateAssignment(Assignment oldAssignment, Assignment newAssignment);
    }
}
