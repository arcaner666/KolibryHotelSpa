namespace Entities.DTOs;

public class InvoiceDto
{
    public long InvoiceId { get; set; }
    public byte InvoiceTypeId { get; set; }
    public byte PaymentTypeId { get; set; }
    public byte CurrencyId { get; set; }
    public string BuyerNameSurname { get; set; }
    public string BuyerEmail { get; set; }
    public string BuyerPhone { get; set; }
    public string Title { get; set; }
    public decimal NetPrice { get; set; }
    public decimal Vat { get; set; }
    public decimal TotalVat { get; set; }
    public decimal TotalPrice { get; set; }
    public bool Paid { get; set; }
    public bool Canceled { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
