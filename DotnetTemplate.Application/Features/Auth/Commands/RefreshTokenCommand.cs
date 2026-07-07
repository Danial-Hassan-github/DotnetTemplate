using DotnetTemplate.Application.DTOs;
using DotnetTemplate.Application.Interfaces;
using MediatR;

namespace DotnetTemplate.Application.Features.Auth.Commands
{
    public record RefreshTokenCommand(string refreshToken) : IRequest<AuthResponse>;
    public class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public RefreshTokenCommandHandler(
            IUserRepository repository,
            IJwtTokenGenerator tokenGenerator)
        {
            _repository = repository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponse> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _repository.GetByRefreshTokenAsync(request.refreshToken);

            if (user == null)
                throw new Exception("Invalid refresh token.");

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
                throw new Exception("Refresh token expired.");

            user.RefreshToken = _tokenGenerator.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _repository.SaveChangesAsync();

            var accessToken = _tokenGenerator.GenerateToken(user);

            return new AuthResponse(accessToken, user.RefreshToken);
        }
    }
}