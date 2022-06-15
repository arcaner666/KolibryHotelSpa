namespace Entities.DTOs;

public class PersonExtDto
{
    public long PersonId { get; set; }
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Role { get; set; } = null!;
    public bool Blocked { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Extended
    public string Password { get; set; } = null!;
    public int RefreshTokenDuration { get; set; }
    public string AccessToken { get; set; } = null!;
}
