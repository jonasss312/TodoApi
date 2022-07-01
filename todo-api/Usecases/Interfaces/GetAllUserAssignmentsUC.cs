using todo_api.Models;

namespace todo_api.Usecases.Interfaces
{
    public interface GetAllUserAssignmentsUC
    {
        Task<IEnumerable<Assignment>> GetAllUserAssignments(int userId);
    }
}
