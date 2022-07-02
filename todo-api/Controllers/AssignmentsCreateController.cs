using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todo_api.Models;
using todo_api.Models.Dtos;
using todo_api.Usecases.Interfaces;

namespace todo_api.Controllers
{
    [Route(Constants.API_PATH + Constants.USERS_PATH + Constants.USER_ID + "/" + Constants.ASSIGNMENTS_PATH)]
    [ApiController]
    [Authorize(Roles = Constants.ADMIN_ROLE + ", " + Constants.USER_ROLE)]
    public class AssignmentsCreateController : ControllerBase
    {
        private readonly CreateAssignmentUC _createAssignmentUC;
        private readonly VerifyUserIdOrAdminUC _verifyUserIdOrAdminUC;

        public AssignmentsCreateController(
            CreateAssignmentUC createAssignmentUC,
            VerifyUserIdOrAdminUC verifyUserIdOrAdminUC)
        {
            _createAssignmentUC = createAssignmentUC;
            _verifyUserIdOrAdminUC = verifyUserIdOrAdminUC;
        }

        [HttpPost]
        public async Task<ActionResult<Assignment>> AddAssignment(int userId, AssignmentDto assignmentDto)
        {
            if(!_verifyUserIdOrAdminUC.VerifyUserIdOrAdmin(User.Claims, userId))
                return BadRequest(Constants.ERROR_BAD_USER);

            if(!ValidateAssignmentData(assignmentDto))
                return BadRequest(Constants.ERROR_ASSIGNMENT_VALIDATION);

            await _createAssignmentUC.CreateAssignment(assignmentDto, userId, out Assignment assignment);
            return Ok(assignment);
        }

        private bool ValidateAssignmentData(AssignmentDto assignmentDto)
        {
            return !String.IsNullOrEmpty(assignmentDto.Name) &&
                typeof(Assignment.StatusType).IsInstanceOfType(assignmentDto.Status);
        }
    }
}
