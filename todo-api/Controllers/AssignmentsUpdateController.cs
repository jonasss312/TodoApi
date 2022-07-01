using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Controllers
{
    [Route(Constants.API_PATH + Constants.USERS_PATH + Constants.USER_ID + "/" + Constants.ASSIGNMENTS_PATH)]
    [ApiController]
    public class AssignmentsUpdateController : ControllerBase
    {
        private readonly GetUserAssignmentUC _getUserAssignmentUC;
        private readonly UpdateAssignmentUC _updateAssignmentUC;

        public AssignmentsUpdateController(
            AssignmentRepo assignmentRepo,
            GetUserAssignmentUC getUserAssignmentUC,
            UpdateAssignmentUC updateAssignmentUC)
        {
            _getUserAssignmentUC = getUserAssignmentUC;
            _updateAssignmentUC = updateAssignmentUC;
        }

        [HttpPatch(Constants.ASSIGNMENT_ID)]
        public async Task<ActionResult<List<Assignment>>> UpdateAssignment(int userId, int assignmentId, Assignment newAssignment)
        {
            // TODO Implement user find
            var foundAssignment = await _getUserAssignmentUC.GetUserAssignment(userId, assignmentId);

            if(foundAssignment == null)
                return BadRequest(Constants.ERROR_ASSIGNMENT_NOT_FOUND);
            if(!ValidateAssignmentData(newAssignment))
                return BadRequest(Constants.ERROR_ASSIGNMENT_VALIDATION);

            await _updateAssignmentUC.UpdateAssignment(foundAssignment, newAssignment);
            return Ok(foundAssignment);
        }

        private bool ValidateAssignmentData(Assignment assignment)
        {
            return String.IsNullOrEmpty(assignment.Name) &&
                typeof(Assignment.StatusType).IsInstanceOfType(assignment.Status);
        }
    }
}
