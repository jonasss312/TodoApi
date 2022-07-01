using todo_api.Models;

namespace todo_api.Usecases.Interfaces
{
    public interface DeleteAssignmentUC
    {
        Task DeleteAssignment(Assignment assignment);
    }
}
