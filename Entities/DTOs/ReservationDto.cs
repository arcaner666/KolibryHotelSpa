namespace Entities.ExtendedDatabaseModels;

public class ReservationDto
{
    public long ReservationId { get; set; }
    public long InvoiceId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public byte Adult { get; set; }
    public byte Child { get; set; }
    public bool Canceled { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
