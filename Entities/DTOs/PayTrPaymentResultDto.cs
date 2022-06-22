namespace Entities.DTOs;

public class PayTrPaymentResultDto
{
    public string MerchantOid { get; set; }
    public string Status { get; set; }
    public string Hash { get; set; }
    public int PaymentAmount { get; set; }
    public int TotalAmount { get; set; }
    public string FailedReasonCode { get; set; }
    public string FailedReasonMessage { get; set; }
    public string PaymentType { get; set; }
    public string Currency { get; set; }
    public int TestMode { get; set; }

}
