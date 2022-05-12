using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dto;

namespace Backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<UserDto> GetAllUsers();
        public Task<bool> TryCreateUserAsync(UserDto userDto);
        public Task<LoginResponceDto> Login(UserDto userModel);
        public LoginResponceDto RefreshToken(LoginResponceDto tokenModel);
    }
}
