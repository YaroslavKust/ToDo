using System.Threading.Tasks;
using ToDo.DataAccess.Interfaces;
using ToDo.DataAccess.Repositories;

namespace ToDo.DataAccess
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ToDoContext _context;

        private IUserRepository _users;

        private IEmployeeRepository _employees;

        private ITaskRepository _tasks;

        public UnitOfWork(ToDoContext context)
        {
            _context = context;
        }

        public IUserRepository Users => _users ??= new UserRepository(_context);
        public IEmployeeRepository Employees => _employees ??= new EmployeeRepository(_context);
        public ITaskRepository Tasks => _tasks ??= new TaskRepository(_context);

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}