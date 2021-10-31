using System.Threading.Tasks;
using ToDo.Entities.DTO;

namespace ToDo.API.Authentication
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuth user);
        string GenerateToken();
        Task<bool> CreateUser(UserForAuth user);
    }
}