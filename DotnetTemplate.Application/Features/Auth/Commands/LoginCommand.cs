using DotnetTemplate.Application.DTOs;
using DotnetTemplate.Application.Interfaces;
using MediatR;

namespace DotnetTemplate.Application.Features.Auth.Commands
{
    public record LoginCommand(LoginRequest loginRequest) : IRequest<AuthResponse>;
    public class LoginCommandHandler
    : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public LoginCommandHandler(
            IUserRepository repository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator tokenGenerator)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.loginRequest;

            var user = await _repository.GetByUsernameAsync(dto.Username);

            if (user == null)
                throw new Exception("Invalid credentials.");

            if (!_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials.");

            user.RefreshToken = _tokenGenerator.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _repository.SaveChangesAsync();

            var accessToken = _tokenGenerator.GenerateToken(user);

            return new AuthResponse(accessToken, user.RefreshToken);
        }
    }
}