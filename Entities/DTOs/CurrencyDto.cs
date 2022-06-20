namespace Entities.DTOs;

public class CurrencyDto
{
    public byte CurrencyId { get; set; }
    public string Title { get; set; }
    public string CurrencySymbol { get; set; }
    public decimal ExchangeRate { get; set; }
}
