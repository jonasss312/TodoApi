using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_api.Data.Repos;
using todo_api.Models;

namespace todo_api.Controllers
{
    [Route("api/user/{userId}/assignment/")]
    [ApiController]
    public class AssignmentController:ControllerBase
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

        [HttpGet("{assigmentId}")]
        public async Task<ActionResult<Assignment>> GetAssignment(int userId, int assignmentId)
        {
            var foundAssignment = _assignmentRepo.Get(userId, assignmentId);
            if(foundAssignment == null)
                return BadRequest("Assignment not found.");
            return Ok(foundAssignment);
        }

        [HttpPost]
        public async Task<ActionResult<Assignment>> AddAssignment(Assignment assignment)
        {
            // TODO Implement user find
            if(String.IsNullOrEmpty(assignment.Name) && typeof(Assignment.StatusType).IsInstanceOfType(assignment.Status))
                return BadRequest("Assignment name and status required");
            await _assignmentRepo.Insert(assignment);
            return Ok(assignment);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<List<Assignment>>> UpdateAssignment(int userId, int assignmentId, Assignment assignment)
        {
            // TODO Implement user find
            var foundAssignment = await _assignmentRepo.Get(userId, assignmentId);
            if(foundAssignment == null)
                return BadRequest("Assignment not found.");
            foundAssignment.Name = assignment.Name;
            foundAssignment.Status = assignment.Status;

            await _assignmentRepo.Update(foundAssignment);
            return Ok(foundAssignment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Assignment>>> UpdateAssignment(int userId, int assignmentId)
        {
            var foundAssignment = await _assignmentRepo.Get(userId, assignmentId);
            if(foundAssignment == null)
                return BadRequest("Assignment not found.");
            await _assignmentRepo.Delete(foundAssignment);
            return NoContent();ted
        }
    }
}
