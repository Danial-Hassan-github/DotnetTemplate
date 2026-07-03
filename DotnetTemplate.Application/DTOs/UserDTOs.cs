namespace DotnetTemplate.Application.DTOs
{
    public record UserResponseDto(int Id, string Name, string Username, string Email);
    public record UserRegisterRequestDto(string Name, string Username, string Email, string Password);
    public record UserLoginRequestDto(string Username, string Email, string Password);
}