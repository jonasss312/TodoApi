using todo_api.Gateway.Interfaces;
using todo_api.Models;

namespace todo_api.Gateway.Implementations
{
    public class AssignmentRepo : AssignmentsGW
    {
        private readonly Context _context;

        public AssignmentRepo(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Assignment>> GetAll(int userId)
        {
            return await _context.Assignments.Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task<Assignment> Get(int userId, int assignmentId)
        {
            return await _context.Assignments.FirstOrDefaultAsync(o => o.Id == assignmentId && o.UserId == userId);
        }

        public async Task Insert(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Assignment assignment)
        {
            _context.Assignments.Update(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Assignment assignment)
        {
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
        }
    }
}
