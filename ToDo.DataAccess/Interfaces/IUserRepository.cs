using System.Threading.Tasks;
using ToDo.Entities.Models;

namespace ToDo.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string login, string password);
    }
}