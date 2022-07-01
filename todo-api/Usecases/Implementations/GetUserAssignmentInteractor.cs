using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class GetUserAssignmentInteractor : GetUserAssignmentUC
    {
        private AssignmentRepo _assignmentRepo;

        public GetUserAssignmentInteractor(AssignmentRepo assignmentRepo)
        {
            _assignmentRepo = assignmentRepo;
        }

        public Task<Assignment> GetUserAssignment(int userId, int assignmentId)
        {
            return _assignmentRepo.Get(userId, assignmentId);
        }
    }
}
