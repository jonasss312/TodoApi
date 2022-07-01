using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Controllers
{
    [Route(Constants.API_PATH + Constants.USERS_PATH + Constants.USER_ID + "/" + Constants.ASSIGNMENTS_PATH)]
    [ApiController]
    public class AssignmentsDeleteController: ControllerBase
    {
        private readonly GetUserAssignmentUC _getUserAssignmentUC;
        private readonly DeleteAssignmentUC _deleteAssignmentUC;

        public AssignmentsDeleteController(
            GetUserAssignmentUC getUserAssignmentUC,
            DeleteAssignmentUC deleteAssignmentUC)
        {
            _getUserAssignmentUC = getUserAssignmentUC;
            _deleteAssignmentUC = deleteAssignmentUC;
        }

        [HttpDelete(Constants.ASSIGNMENT_ID)]
        public async Task<ActionResult<List<Assignment>>> DeleteAssignment(int userId, int assignmentId)
        {
            var foundAssignment = await _getUserAssignmentUC.GetUserAssignment(userId, assignmentId);

            if(foundAssignment == null)
                return BadRequest(Constants.ERROR_ASSIGNMENT_NOT_FOUND);

            await _deleteAssignmentUC.DeleteAssignment(foundAssignment);
            return NoContent();
        }
    }
}
