using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class DeleteAssignmentInteractor : DeleteAssignmentUC
    {
        private AssignmentRepo _assignmentRepo;

        public DeleteAssignmentInteractor(AssignmentRepo assignmentRepo)
        {
            _assignmentRepo = assignmentRepo;
        }

        public Task DeleteAssignment(Assignment assignment)
        {
            return _assignmentRepo.Delete(assignment);
        }
    }
}
