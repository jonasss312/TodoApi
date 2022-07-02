using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Usecases.Interfaces;
using static todo_api.Models.User;

namespace todo_api.Controllers
{
    [Route(Constants.API_PATH + Constants.USERS_PATH + Constants.USER_ID + "/" + Constants.ASSIGNMENTS_PATH)]
    [ApiController]
    [Authorize(Roles = Constants.ADMIN_ROLE + ", " + Constants.USER_ROLE)]
    public class AssignmentsGetController: ControllerBase
    {
        private readonly GetAllUserAssignmentsUC _getAllUserAssignmentsUC;
        private readonly GetUserAssignmentUC _getUserAssignmentUC;
        private readonly VerifyAssignmentCreatorOrAdminUC _verifyAssignmentCreatorOrAdminUC;
        private readonly VerifyUserIdOrAdminUC _verifyUserIdOrAdminUC;

        public AssignmentsGetController(
            GetAllUserAssignmentsUC getAllUserAssignmentsUC,
            GetUserAssignmentUC getUserAssignmentUC,
            VerifyAssignmentCreatorOrAdminUC verifyAssignmentCreatorOrAdminUC,
            VerifyUserIdOrAdminUC verifyUserIdOrAdminUC)
        {
            _getAllUserAssignmentsUC = getAllUserAssignmentsUC;
            _getUserAssignmentUC= getUserAssignmentUC;
            _verifyAssignmentCreatorOrAdminUC = verifyAssignmentCreatorOrAdminUC;
            _verifyUserIdOrAdminUC = verifyUserIdOrAdminUC;
        }

        [HttpGet]
        public async Task<ActionResult<List<Assignment>>> GetAll(int userId)
        {
            if (!_verifyUserIdOrAdminUC.VerifyUserIdOrAdmin(User.Claims, userId))
                return BadRequest(Constants.ERROR_BAD_USER);
            return Ok(await _getAllUserAssignmentsUC.GetAllUserAssignments(userId));
        }

        [HttpGet(Constants.ASSIGNMENT_ID)]
        public async Task<ActionResult<Assignment>> GetAssignment(int userId, int assignmentId)
        {
            if(!_verifyUserIdOrAdminUC.VerifyUserIdOrAdmin(User.Claims, userId))
                return BadRequest(Constants.ERROR_BAD_USER);

            var foundAssignment = await _getUserAssignmentUC.GetUserAssignment(userId, assignmentId);

            if(foundAssignment == null)
                return BadRequest(Constants.ERROR_ASSIGNMENT_NOT_FOUND);
            if(!_verifyAssignmentCreatorOrAdminUC.VerifyAssignmentCreatorOrAdmin(User.Claims, foundAssignment.UserId))
                return BadRequest(Constants.ERROR_NOT_ASSIGNMENT_CREATOR);


            return Ok(foundAssignment);
        }
    }
}
