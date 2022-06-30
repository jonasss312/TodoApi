using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_api.Models;

namespace todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController:ControllerBase
    {
        private List<Assignment> tasks = new List<Assignment>
            {
                new Assignment{
                    Id = 1,
                    Name = "lol",
                    Status = Assignment.StatusType.Done
                }
            };

        [HttpGet]
        public async Task<ActionResult<List<Assignment>>> Get()
        {
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> GetAssignment(int id)
        {
            Assignment assignment = tasks.Find(a => a.Id == id);
            if(assignment == null)
                return BadRequest("Assignment not found.");
            return Ok(assignment);
        }

        [HttpPost]
        public async Task<ActionResult<List<Assignment>>> AddAssignment(Assignment assignment)
        {
            tasks.Add(assignment);
            return Ok(tasks);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<List<Assignment>>> UpdateAssignment(int id, Assignment assignment)
        {
            Assignment foundAssignment = tasks.Find(a => a.Id == id);
            if(foundAssignment == null)
                return BadRequest("Assignment not found.");

            foundAssignment.Name = assignment.Name;
            foundAssignment.Status = assignment.Status;

            return Ok(tasks);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Assignment>>> UpdateAssignment(int id)
        {
            Assignment foundAssignment = tasks.Find(a => a.Id == id);
            if(foundAssignment == null)
                return BadRequest("Assignment not found.");
            tasks.Remove(foundAssignment);
            return Ok(foundAssignment);
        }
    }
}
