namespace Entities.DTOs;

public class PayTrIframeDto
{
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int PaymentAmount { get; set; }
    public List<InvoiceDetailDto> UserBasket { get; set; }
    public string MerchantOid { get; set; }
    public string MerchantOkUrl { get; set; }
    public string MerchantFailUrl { get; set; }
    public string UserIp { get; set; }
    public string Currency { get; set; }
    public string Language { get; set; }
    public string IframeToken { get; set; }
}
