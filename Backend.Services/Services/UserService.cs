using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Shared.Dto;

namespace Backend.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public async Task<bool> TryCreateUser(UserDto userModel)
        {
            return await _userRepository.TryCreateUserAsync(userModel);
        }

        public async Task<LoginResponceDto> Login(UserDto userModel)
        {
            return await _userRepository.Login(userModel);
        }

        public LoginResponceDto RefreshToken(LoginResponceDto userModel)
        {
            return _userRepository.RefreshToken(userModel);
        }
    }
}