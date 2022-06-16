namespace Entities.DTOs;

public class SuiteDto
{
    public int SuiteId { get; set; }
    public string Title { get; set; }
    public byte Bed { get; set; }
    public short M2 { get; set; }
    public decimal Price { get; set; }
    public byte Vat { get; set; }
    public decimal TotalVat { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
