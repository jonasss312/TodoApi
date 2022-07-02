using todo_api.Gateway.Interfaces;
using todo_api.Models;
using todo_api.Models.Dtos;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class UpdateAssignmentInteractor : UpdateAssignmentUC
    {
        private AssignmentsGW _assignmentsGW;

        public UpdateAssignmentInteractor(AssignmentsGW assignmentsGW)
        {
            _assignmentsGW = assignmentsGW;
        }

        public Task UpdateAssignment(Assignment oldAssignment, AssignmentDto newAssignment)
        {
            oldAssignment.Name = newAssignment.Name;
            oldAssignment.Status = newAssignment.Status;

            return _assignmentsGW.Update(oldAssignment);
        }
    }
}
