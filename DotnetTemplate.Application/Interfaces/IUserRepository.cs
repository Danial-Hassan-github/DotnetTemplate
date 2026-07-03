using DotnetTemplate.Application.DTOs;
using DotnetTemplate.Domain.Entities;

namespace DotnetTemplate.Application.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<UserResponseDto> RegisterUserAsync(UserRegisterRequestDto registerRequest);
        public Task<UserResponseDto> LoginUser(UserLoginRequestDto loginRequest);
    }
}