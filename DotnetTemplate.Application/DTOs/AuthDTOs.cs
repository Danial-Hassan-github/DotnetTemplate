namespace DotnetTemplate.Application.DTOs
{
    public record AuthResponse(string Token, string RefreshToken);
    public record RegisterRequest(string Name, string Username, string Email, string Password);
    public record LoginRequest(string Username, string Email, string Password);
}