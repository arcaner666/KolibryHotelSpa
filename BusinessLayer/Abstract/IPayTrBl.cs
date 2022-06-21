using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface IPayTrBl
{
    IDataResult<PayTrIframeDto> GetIframeToken(PayTrIframeDto payTrIframeDto);
}
