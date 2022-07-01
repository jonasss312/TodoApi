using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Controllers
{
    [Route(Constants.API_PATH + Constants.USERS_PATH + Constants.USER_ID + "/" + Constants.ASSIGNMENTS_PATH)]
    [ApiController]
    public class AssignmentsCreateController : ControllerBase
    {
        private readonly CreateAssignmentUC _createAssignmentUC;

        public AssignmentsCreateController(CreateAssignmentUC createAssignmentUC)
        {
            _createAssignmentUC = createAssignmentUC;
        }

        [HttpPost]
        public async Task<ActionResult<Assignment>> AddAssignment(int userId, Assignment assignment)
        {
            // TODO Implement user find
            if(!ValidateAssignmentData(assignment))
                return BadRequest(Constants.ERROR_ASSIGNMENT_VALIDATION);

            await _createAssignmentUC.CreateAssignment(assignment);
            return Ok(assignment);
        }

        private bool ValidateAssignmentData(Assignment assignment)
        {
            return String.IsNullOrEmpty(assignment.Name) &&
                typeof(Assignment.StatusType).IsInstanceOfType(assignment.Status);
        }
    }
}
