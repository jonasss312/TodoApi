using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Usecases.Interfaces
{
    public interface UpdateAssignmentUC
    {
        Task UpdateAssignment(Assignment oldAssignment, AssignmentDto newAssignment);
    }
}
