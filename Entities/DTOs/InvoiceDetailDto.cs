namespace Entities.DTOs;

public class InvoiceDetailDto
{
    public long InvoiceDetailId { get; set; }
    public long InvoiceId { get; set; }
    public int SuiteId { get; set; }
    public byte Amount { get; set; }
    public decimal Price { get; set; }
    public byte Vat { get; set; }
    public decimal TotalVat { get; set; }
    public decimal TotalPrice { get; set; }
}
