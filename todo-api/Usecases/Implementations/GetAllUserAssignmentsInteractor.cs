using todo_api.Gateway.Interfaces;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class GetAllUserAssignmentsInteractor : GetAllUserAssignmentsUC
    {
        private AssignmentsGW _assignmentsGW;

        public GetAllUserAssignmentsInteractor(AssignmentsGW assignmentsGW)
        {
            _assignmentsGW = assignmentsGW;
        }

        public Task<IEnumerable<Assignment>> GetAllUserAssignments(int userId)
        {
            return _assignmentsGW.GetAll(userId);
        }
    }
}
