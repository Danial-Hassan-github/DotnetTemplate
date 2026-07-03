using DotnetTemplate.Application.DTOs;
using DotnetTemplate.Application.Interfaces;
using MediatR;

namespace DotnetTemplate.Application.Features.Users.Commands
{
    public record UserLoginCommand(UserLoginRequestDto loginRequest) : IRequest<UserResponseDto>;
    public class LoginUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UserLoginCommand, UserResponseDto>
    {
        public async Task<UserResponseDto> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            return await userRepository.LoginUser(request.loginRequest);
        }
    }
}