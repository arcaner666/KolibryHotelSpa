namespace Entities.DTOs;

public class InvoiceExtDto
{
    public long InvoiceId { get; set; }
    public byte CurrencyId { get; set; }
    public string BuyerNameSurname { get; set; }
    public string BuyerEmail { get; set; }
    public string BuyerPhone { get; set; }
    public DateTimeOffset ReservationStartDate { get; set; }
    public DateTimeOffset ReservationEndDate { get; set; }
    public byte Adult { get; set; }
    public byte Child { get; set; }
    public byte ChildAge1 { get; set; }
    public byte ChildAge2 { get; set; }
    public byte ChildAge3 { get; set; }
    public byte ChildAge4 { get; set; }
    public byte ChildAge5 { get; set; }
    public byte ChildAge6 { get; set; }
    public string Title { get; set; }
    public decimal NetPrice { get; set; }
    public decimal Vat { get; set; }
    public decimal TotalVat { get; set; }
    public decimal TotalPrice { get; set; }
    public bool Paid { get; set; }
    public bool Canceled { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Extended With InvoiceType
    // Extended With PaymentType
    // Extended With Currency

    // Extended With InvoiceDetail
    public List<InvoiceDetailDto> InvoiceDetailDtos { get; set; }
}
