using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dto;

namespace Backend.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserDto> GetAllUsers();
        public Task<bool> TryCreateUser(UserDto userModel);
        public Task<LoginResponceDto> Login(UserDto userModel);
        public LoginResponceDto RefreshToken(LoginResponceDto userModel);
    }
}
