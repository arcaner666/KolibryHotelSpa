namespace Entities.DTOs;

public class InvoiceDto
{
    public long InvoiceId { get; set; }
    public byte InvoiceTypeId { get; set; }
    public byte PaymentTypeId { get; set; }
    public byte CurrencyId { get; set; }
    public string BuyerNameSurname { get; set; } = null!;
    public string BuyerEmail { get; set; } = null!;
    public string BuyerPhone { get; set; } = null!;
    public string Title { get; set; } = null!;
    public decimal NetPrice { get; set; }
    public byte Vat { get; set; }
    public decimal TotalVat { get; set; }
    public decimal TotalPrice { get; set; }
    public bool Paid { get; set; }
    public bool Canceled { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
