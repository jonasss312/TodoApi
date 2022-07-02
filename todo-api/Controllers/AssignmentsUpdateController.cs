using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Models.Dtos;
using todo_api.Usecases.Interfaces;

namespace todo_api.Controllers
{
    [Route(Constants.API_PATH + Constants.USERS_PATH + Constants.USER_ID + "/" + Constants.ASSIGNMENTS_PATH)]
    [ApiController]
    [Authorize(Roles = Constants.USER_ROLE)]
    public class AssignmentsUpdateController : ControllerBase
    {
        private readonly GetUserAssignmentUC _getUserAssignmentUC;
        private readonly UpdateAssignmentUC _updateAssignmentUC;
        private readonly VerifyAssignmentCreatorOrAdminUC _verifyAssignmentCreatorOrAdminUC;

        public AssignmentsUpdateController(
            GetUserAssignmentUC getUserAssignmentUC,
            UpdateAssignmentUC updateAssignmentUC,
            VerifyAssignmentCreatorOrAdminUC verifyAssignmentCreatorOrAdminUC)
        {
            _getUserAssignmentUC = getUserAssignmentUC;
            _updateAssignmentUC = updateAssignmentUC;
            _verifyAssignmentCreatorOrAdminUC = verifyAssignmentCreatorOrAdminUC;
        }

        [HttpPatch(Constants.ASSIGNMENT_ID)]
        public async Task<ActionResult<List<Assignment>>> UpdateAssignment(int userId, int assignmentId, AssignmentDto newAssignment)
        {
            var foundAssignment = await _getUserAssignmentUC.GetUserAssignment(userId, assignmentId);

            if(foundAssignment == null)
                return BadRequest(Constants.ERROR_ASSIGNMENT_NOT_FOUND);
            if(!_verifyAssignmentCreatorOrAdminUC.VerifyAssignmentCreatorOrAdmin(User.Claims, foundAssignment.UserId))
                return BadRequest(Constants.ERROR_NOT_ASSIGNMENT_CREATOR);
            if(!ValidateAssignmentData(newAssignment))
                return BadRequest(Constants.ERROR_ASSIGNMENT_VALIDATION);

            await _updateAssignmentUC.UpdateAssignment(foundAssignment, newAssignment);
            return Ok(foundAssignment);
        }

        private bool ValidateAssignmentData(AssignmentDto newAssignment)
        {
            return !String.IsNullOrEmpty(newAssignment.Name) &&
                typeof(Assignment.StatusType).IsInstanceOfType(newAssignment.Status);
        }
    }
}
