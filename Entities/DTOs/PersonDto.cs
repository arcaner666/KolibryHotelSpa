namespace Entities.DTOs;

public class PersonDto
{
    public long PersonId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string Role { get; set; }
    public bool Blocked { get; set; }
    public string RefreshToken { get; set; }
    public DateTimeOffset RefreshTokenExpiryTime { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Extended
    public string Password { get; set; }
}
