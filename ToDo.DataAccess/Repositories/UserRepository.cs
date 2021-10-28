using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Models;

namespace ToDo.DataAccess.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(ToDoContext context):base(context){ }

        public async Task<User> GetUserAsync(string login, string password) =>
            await GetByConditions(u => u.Name == login && u.Password == password).FirstOrDefaultAsync();
    }
}