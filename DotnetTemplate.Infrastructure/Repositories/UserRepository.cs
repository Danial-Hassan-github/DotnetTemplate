using DotnetTemplate.Application.DTOs;
using DotnetTemplate.Application.Interfaces;
using DotnetTemplate.Domain.Entities;
using DotnetTemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DotnetTemplate.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly MyDbContext _context;
        public readonly IPasswordHasher _passwordHasher;
        public UserRepository(MyDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public async Task<UserResponseDto> RegisterUserAsync(UserRegisterRequestDto registerRequest)
        {
            User newUser = new User();
            newUser.Username = registerRequest.Username;
            newUser.Name = registerRequest.Name;
            newUser.Email = registerRequest.Email;
            newUser.PasswordHash = _passwordHasher.HashPassword(registerRequest.Password);
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return new UserResponseDto(newUser.Id, newUser.Name, newUser.Username, newUser.Email);
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponseDto> LoginUser(UserLoginRequestDto loginRequest)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginRequest.Username || u.Email == loginRequest.Email);
            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = _passwordHasher.VerifyPassword(loginRequest.Password.Trim(), user.PasswordHash);

            if (!isPasswordValid)
            {
                return null;
            }
            return new UserResponseDto(user.Id, user.Name, user.Username, user.Email);
        }

    }
}