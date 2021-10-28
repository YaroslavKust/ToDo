using System.Threading.Tasks;

namespace ToDo.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get;}
        IEmployeeRepository Employees { get;}
        ITaskRepository Tasks { get;}
        Task SaveAsync();
    }
}