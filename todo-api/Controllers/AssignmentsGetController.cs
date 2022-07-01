using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Controllers
{
    [Route(Constants.API_PATH + Constants.USERS_PATH + Constants.USER_ID + "/" + Constants.ASSIGNMENTS_PATH)]
    [ApiController]
    public class AssignmentsGetController: ControllerBase
    {
        private readonly GetAllUserAssignmentsUC _getAllUserAssignmentsUC;
        private readonly GetUserAssignmentUC _getUserAssignmentUC;

        public AssignmentsGetController(
            AssignmentRepo assignmentRepo,
            GetAllUserAssignmentsUC getAllUserAssignmentsUC, 
            GetUserAssignmentUC getUserAssignmentUC)
        {
            _getAllUserAssignmentsUC = getAllUserAssignmentsUC;
            _getUserAssignmentUC= getUserAssignmentUC;
        }

        [HttpGet]
        public async Task<ActionResult<List<Assignment>>> GetAll(int userId)
        {
            return Ok(await _getAllUserAssignmentsUC.GetAllUserAssignments(userId));
        }

        [HttpGet(Constants.ASSIGNMENT_ID)]
        public async Task<ActionResult<Assignment>> GetAssignment(int userId, int assignmentId)
        {
            var foundAssignment = await _getUserAssignmentUC.GetUserAssignment(userId, assignmentId);

            if(foundAssignment == null)
                return BadRequest(Constants.ERROR_ASSIGNMENT_NOT_FOUND);

            return Ok(foundAssignment);
        }
    }
}
