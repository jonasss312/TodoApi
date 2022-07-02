using todo_api.Gateway.Interfaces;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class DeleteAssignmentInteractor : DeleteAssignmentUC
    {
        private AssignmentsGW _assignmentsGW;

        public DeleteAssignmentInteractor(AssignmentsGW assignmentsGW)
        {
            _assignmentsGW = assignmentsGW;
        }

        public Task DeleteAssignment(Assignment assignment)
        {
            return _assignmentsGW.Delete(assignment);
        }
    }
}
