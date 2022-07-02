using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Gateway
{
    public class DatabaseSeeder
    {
        private readonly Context _context;
        private readonly CreatePasswordHashUC _createPasswordHashUC;

        public DatabaseSeeder(Context context, CreatePasswordHashUC createPasswordHashUC)
        {
            _context = context;
            _createPasswordHashUC = createPasswordHashUC;
        }

        public void Seed()
        {
            DeleteAllUsers();
            SeedUsers();
            SeedAssignments();
        }

        private void DeleteAllUsers()
        {
            foreach(var user in _context.Users)
            {
                _context.Users.Remove(user);
            }
            foreach(var assignments in _context.Assignments)
            {
                _context.Assignments.Remove(assignments);
            }
            _context.SaveChanges();
        }

        private void SeedUsers()
        {
            /* 
             * {
                "email": "admin@admin",
                "password": "verylongpassword"
                }
            */
            _createPasswordHashUC.CreatePasswordHash("verylongpassword", out byte[] passwordHash, out byte[] passwordSalt);
            _context.Users.Add(new User
            {
                Email = "admin@admin",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Constants.ADMIN_ROLE
            });
            /*
             * {
                "email": "user1@mail.com",
                "password": "verylongpassword"
                }
            */
            _context.Users.Add(new User
            {
                Email = "user1@mail.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Constants.USER_ROLE
            });
            /*
             * {
                "email": "user2@mail.com",
                "password": "verylongpassword"
                }
            */
            _context.Users.Add(new User
            {
                Email = "user2@mail.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Constants.USER_ROLE
            });
            /*
             * {
                "email": "7874694984264a@gmail.com",
                "password": "verylongpassword"
                }
            */
            _context.Users.Add(new User
            {
                Email = "7874694984264a@gmail.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Constants.USER_ROLE
            });
            _context.SaveChanges();
        }

        private void SeedAssignments()
        {
            IList<int> userIds = _context.Users.Select(u => u.Id).ToList();
            _context.Assignments.Add(
                new Assignment
                {
                    Name = "Do homework.",
                    Status = Assignment.StatusType.InProgress,
                    UserId = userIds[1]
                });
            _context.Assignments.Add(
                new Assignment
                {
                    Name = "Cool todo task.",
                    Status = Assignment.StatusType.InProgress,
                    UserId = userIds[1]
                });
            _context.Assignments.Add(
                new Assignment
                {
                    Name = "Special business to do list apps allow editing and rearranging to-dos according to priorities, sharing lists with team members or other collaborators, and getting reminders for your upcoming deadlines. Paperless. No matter which device you have at the moment.",
                    Status = Assignment.StatusType.Done,
                    UserId = userIds[1]
                });
            _context.Assignments.Add(
                new Assignment
                {
                    Name = "Managing various tasks in an app is more efficient. This is a better way to be more productive than doing it on paper. If you do not agree or have some hesitations – this post will offer some best examples of online to-do list solutions for any business and private aims. Let’s get to know more about them!",
                    Status = Assignment.StatusType.InProgress,
                    UserId = userIds[2]
                });
            _context.Assignments.Add(
                new Assignment
                {
                    Name = "There are hundreds of smart to-do list solutions out there. Some of them seem too limited while some just perform better than others. ",
                    Status = Assignment.StatusType.Done,
                    UserId = userIds[3]
                });
            _context.Assignments.Add(
                new Assignment
                {
                    Name = "Managing various tasks in an app is more efficient. This is a better way to be more productive than doing it on paper. If you do not agree or have some hesitations – this post will offer some best examples of online to-do list solutions for any business and private aims. Let’s get to know more about them!",
                    Status = Assignment.StatusType.InProgress,
                    UserId = userIds[2]
                });
            _context.SaveChanges();
        }
    }
}
