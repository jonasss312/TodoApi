using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class CreateAssignmentInteractor : CreateAssignmentUC
    {
        private AssignmentRepo _assignmentRepo;

        public CreateAssignmentInteractor(AssignmentRepo assignmentRepo)
        {
            _assignmentRepo = assignmentRepo;
        }

        public Task CreateAssignment(Assignment assignment, int userId)
        {
            assignment.UserId = userId;
            return _assignmentRepo.Insert(assignment);
        }
    }
}
