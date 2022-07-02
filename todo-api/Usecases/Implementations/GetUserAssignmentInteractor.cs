using todo_api.Gateway.Interfaces;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class GetUserAssignmentInteractor : GetUserAssignmentUC
    {
        private AssignmentsGW _assignmentsGW;

        public GetUserAssignmentInteractor(AssignmentsGW assignmentsGW)
        {
            _assignmentsGW = assignmentsGW;
        }

        public Task<Assignment> GetUserAssignment(int userId, int assignmentId)
        {
            return _assignmentsGW.Get(userId, assignmentId);
        }
    }
}
