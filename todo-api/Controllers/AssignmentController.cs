using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_api.Data.Repos;
using todo_api.Models;

namespace todo_api.Controllers
{
    [Route(Constants.API_PATH + Constants.USERS_PATH + Constants.USER_ID + "/" + Constants.ASSIGNMENTS_PATH)]
    [ApiController]
    public class AssignmentController: ControllerBase
    {
        private readonly AssignmentRepo _assignmentRepo;

        public AssignmentController(AssignmentRepo assignmentRepo)
        {
            _assignmentRepo = assignmentRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Assignment>>> GetAll(int userId)
        {
            return Ok(await _assignmentRepo.GetAll(userId));
        }

        [HttpGet(Constants.ASSIGNMENT_ID)]
        public async Task<ActionResult<Assignment>> GetAssignment(int userId, int assignmentId)
        {
            var foundAssignment = _assignmentRepo.Get(userId, assignmentId);

            if(foundAssignment == null)
                return BadRequest(Constants.ERROR_ASSIGNMENT_NOT_FOUND);

            return Ok(foundAssignment);
        }

        [HttpPost]
        public async Task<ActionResult<Assignment>> AddAssignment(Assignment assignment)
        {
            // TODO Implement user find
            if(!ValidateAssignmentData(assignment))
                return BadRequest(Constants.ERROR_ASSIGNMENT_VALIDATION);

            await _assignmentRepo.Insert(assignment);
            return Ok(assignment);
        }

        [HttpPatch(Constants.ASSIGNMENT_ID)]
        public async Task<ActionResult<List<Assignment>>> UpdateAssignment(int userId, int assignmentId, Assignment assignment)
        {
            // TODO Implement user find
            var foundAssignment = await _assignmentRepo.Get(userId, assignmentId);

            if(foundAssignment == null)
                return BadRequest(Constants.ERROR_ASSIGNMENT_NOT_FOUND);

            if(!ValidateAssignmentData(assignment))
                return BadRequest(Constants.ERROR_ASSIGNMENT_VALIDATION);

            foundAssignment.Name = assignment.Name;
            foundAssignment.Status = assignment.Status;

            await _assignmentRepo.Update(foundAssignment);
            return Ok(foundAssignment);
        }

        private bool ValidateAssignmentData(Assignment assignment)
        {
            return String.IsNullOrEmpty(assignment.Name) && 
                typeof(Assignment.StatusType).IsInstanceOfType(assignment.Status);
        }

        [HttpDelete(Constants.ASSIGNMENT_ID)]
        public async Task<ActionResult<List<Assignment>>> DeleteAssignment(int userId, int assignmentId)
        {
            var foundAssignment = await _assignmentRepo.Get(userId, assignmentId);
            if(foundAssignment == null)
                return BadRequest(Constants.ERROR_ASSIGNMENT_NOT_FOUND);
            await _assignmentRepo.Delete(foundAssignment);
            return NoContent();
        }
    }
}
