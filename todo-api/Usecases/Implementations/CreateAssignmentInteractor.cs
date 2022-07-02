using todo_api.Gateway.Interfaces;
using todo_api.Models;
using todo_api.Models.Dtos;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class CreateAssignmentInteractor : CreateAssignmentUC
    {
        private AssignmentsGW _assignmentsGW;

        public CreateAssignmentInteractor(AssignmentsGW assignmentsGW)
        {
            _assignmentsGW = assignmentsGW;
        }

        public Task CreateAssignment(AssignmentDto assignmentDto, int userId, out Assignment assignment)
        {
            assignment = new Assignment
            {
                Name = assignmentDto.Name,
                Status = assignmentDto.Status,
                UserId = userId
            };

            return _assignmentsGW.Insert(assignment);
        }
    }
}
