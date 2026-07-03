using DotnetTemplate.Application.DTOs;
using DotnetTemplate.Application.Interfaces;
using MediatR;

namespace DotnetTemplate.Application.Features.Users.Commands
{
    public record UserRegisterCommand(UserRegisterRequestDto registerRequest) : IRequest<UserResponseDto>;
    public class RegisterUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UserRegisterCommand, UserResponseDto>
    {
        public async Task<UserResponseDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            return await userRepository.RegisterUserAsync(request.registerRequest);
        }
    }
}