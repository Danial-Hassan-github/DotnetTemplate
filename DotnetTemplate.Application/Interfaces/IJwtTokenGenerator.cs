using DotnetTemplate.Domain.Entities;

namespace DotnetTemplate.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(User user);
        public string GenerateRefreshToken();
    }
}