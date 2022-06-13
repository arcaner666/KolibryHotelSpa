using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface IClaimBl
{
    IDataResult<List<ClaimDto>> GetAll();
    IDataResult<ClaimDto> GetByTitle(string title);
}
