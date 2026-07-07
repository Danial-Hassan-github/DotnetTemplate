using DotnetTemplate.Application.DTOs;
using DotnetTemplate.Application.Interfaces;
using DotnetTemplate.Domain.Entities;
using MediatR;

namespace DotnetTemplate.Application.Features.Auth.Commands
{
    public record RegisterCommand(RegisterRequest registerRequest) : IRequest<AuthResponse>;
    public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public RegisterCommandHandler(
            IUserRepository repository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator tokenGenerator)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponse> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.registerRequest;

            if (await _repository.GetByEmailAsync(dto.Email) != null)
                throw new Exception("Email already exists.");

            if (await _repository.GetByUsernameAsync(dto.Username) != null)
                throw new Exception("Username already exists.");

            var user = new User
            {
                Name = dto.Name,
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = _passwordHasher.HashPassword(dto.Password),
                RefreshToken = _tokenGenerator.GenerateRefreshToken(),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
            };

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            var accessToken = _tokenGenerator.GenerateToken(user);

            return new AuthResponse(accessToken, user.RefreshToken);
        }
    }
}