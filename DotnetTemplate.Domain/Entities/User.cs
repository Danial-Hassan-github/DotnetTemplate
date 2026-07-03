namespace DotnetTemplate.Domain.Entities;

public partial class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}