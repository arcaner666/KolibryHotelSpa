using BusinessLayer.Utilities.Results;
using Entities.DTOs;
using Entities.ExtendedDatabaseModels;

namespace BusinessLayer.Abstract;

public interface IPersonClaimBl
{
    IDataResult<PersonClaimDto> Add(PersonClaimDto personClaimDto);
    IDataResult<List<PersonClaimExtDto>> GetExtsByPersonId(long personId);
}
