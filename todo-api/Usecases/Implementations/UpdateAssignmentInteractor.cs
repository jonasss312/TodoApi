using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Models.Dtos;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class UpdateAssignmentInteractor : UpdateAssignmentUC
    {
        private AssignmentRepo _assignmentRepo;

        public UpdateAssignmentInteractor(AssignmentRepo assignmentRepo)
        {
            _assignmentRepo = assignmentRepo;
        }

        public Task UpdateAssignment(Assignment oldAssignment, AssignmentDto newAssignment)
        {
            oldAssignment.Name = newAssignment.Name;
            oldAssignment.Status = newAssignment.Status;

            return _assignmentRepo.Update(oldAssignment);
        }
    }
}
