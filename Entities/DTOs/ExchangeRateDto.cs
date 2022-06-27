namespace Entities.DTOs;

public class ExchangeRateDto
{
    public string Kodu { get; set; }
    public string Adi { get; set; }
    public int Birimi { get; set; }
    public decimal AlisKuru { get; set; }
    public decimal SatisKuru { get; set; }
    public decimal EfektifAlisKuru { get; set; }
    public decimal EfektifSatisKuru { get; set; }
}
