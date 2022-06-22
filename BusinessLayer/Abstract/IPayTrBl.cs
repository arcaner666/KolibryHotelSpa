using BusinessLayer.Utilities.Results;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Abstract;

public interface IPayTrBl
{
    IDataResult<PayTrIframeDto> GetIframeToken(PayTrIframeDto payTrIframeDto);
    string SetPaymentResult(IFormCollection form);
}
