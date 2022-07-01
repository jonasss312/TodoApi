using Microsoft.EntityFrameworkCore;
using todo_api.Models;

namespace todo_api.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
