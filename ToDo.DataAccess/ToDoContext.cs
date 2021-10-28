using Microsoft.EntityFrameworkCore;
using ToDo.Entities.Models;

namespace ToDo.DataAccess
{
    public class ToDoContext: DbContext
    {
        public ToDoContext(DbContextOptions options) : base(options){ }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<MyTask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}