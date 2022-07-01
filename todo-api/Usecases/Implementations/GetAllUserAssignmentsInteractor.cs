using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class GetAllUserAssignmentsInteractor : GetAllUserAssignmentsUC
    {
        private AssignmentRepo _assignmentRepo;

        public GetAllUserAssignmentsInteractor(AssignmentRepo assignmentRepo)
        {
            _assignmentRepo = assignmentRepo;
        }

        public Task<IEnumerable<Assignment>> GetAllUserAssignments(int userId)
        {
            return _assignmentRepo.GetAll(userId);
        }
    }
}
