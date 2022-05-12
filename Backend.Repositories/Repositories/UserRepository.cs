using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Backend.Data.Context;
using Backend.Data.Models;
using Backend.Repositories.Interfaces;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;

namespace Backend.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _dbContext;

        public UserRepository(DataBaseContext dataBaseContext)
        {
            _dbContext = dataBaseContext;
            _dbContext.Database.EnsureCreated();
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var userList = _dbContext.UserModels.ToList();
            var userResult = new List<UserDto>();
            foreach (var user in userList)
            {
                userResult.Add(new UserDto()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password,
                    Permission = user.Permission.Adapt<Permission>()
                });
            }
            return userResult;
        }

        public async Task<bool> TryCreateUserAsync(UserDto userDto)
        {
            if (_dbContext.UserModels.FirstOrDefault(t => t.Login == userDto.Login) != null) return false;

            var user = new UserModel()
            {
                Email = userDto.Email,
                Login = userDto.Login,
                Password = EncryptPass(userDto.Password),
                PermissionId = (int) userDto.Permission
            };
            await _dbContext.UserModels.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<LoginResponceDto> Login(UserDto userModel)
        {
            var userDb = await _dbContext.UserModels.FirstOrDefaultAsync(t => t.Login == userModel.Login);

            if (userDb == null)
            {
                throw new Exception();
            }

            if (userDb.Password == EncryptPass(userModel.Password))
            {
                return new LoginResponceDto
                {
                    Login = userModel.Login, RefreshToken = GenerateRefreshToken(), Token = GenerateToken(userDb)
                };
            }

            throw new Exception();
        }

        public LoginResponceDto RefreshToken(LoginResponceDto tokenModel)
        {
            if (tokenModel is null)
            {
                return null;
            }

            if (_dbContext.UserModels.FirstOrDefault(t => t.Login == tokenModel.Login) == null) return null;

            return new LoginResponceDto
            {
                Login = tokenModel.Login,
                RefreshToken = GenerateRefreshToken(),
                Token = GenerateToken(_dbContext.UserModels.FirstOrDefault(t => t.Login == tokenModel.Login))
            };
        }

        private const string Secret =
            "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        public static string EncryptPass(string password)
        {
            var msg = "";
            var encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }

        private static string GenerateToken(UserModel user)
        {
            var permission = (Permission) user.PermissionId;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Login), new Claim(ClaimTypes.Role, permission.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}