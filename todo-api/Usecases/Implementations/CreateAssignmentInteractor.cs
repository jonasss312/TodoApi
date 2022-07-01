using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Models.Dtos;
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

        public Task CreateAssignment(AssignmentDto assignmentDto, int userId, out Assignment assignment)
        {
            assignment = new Assignment
            {
                Name = assignmentDto.Name,
                Status = assignmentDto.Status,
                UserId = userId
            };

            return _assignmentRepo.Insert(assignment);
        }
    }
}
