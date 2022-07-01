using todo_api.Models;

namespace todo_api.Usecases.Interfaces
{
    public interface CreateAssignmentUC
    {
        Task CreateAssignment(Assignment assignment);
    }
}
