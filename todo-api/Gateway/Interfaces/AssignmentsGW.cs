using todo_api.Models;

namespace todo_api.Gateway.Interfaces
{
    public interface AssignmentsGW
    {
        Task<IEnumerable<Assignment>> GetAll(int userId);

        Task<Assignment> Get(int userId, int assignmentId);

        Task Insert(Assignment assignment);

        Task Update(Assignment assignment);

        Task Delete(Assignment assignment);
    }
}
