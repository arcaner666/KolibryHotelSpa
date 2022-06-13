namespace Entities.ExtendedDatabaseModels;

public class PersonClaimExt
{
    public long PersonClaimId { get; set; }
    public long PersonId { get; set; }
    public int ClaimId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Extended With Claim
    public string ClaimTitle { get; set; }
}
