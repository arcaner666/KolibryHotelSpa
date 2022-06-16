using BusinessLayer.Utilities.Results;
using Entities.DTOs;
using Entities.ExtendedDatabaseModels;

namespace BusinessLayer.Abstract;

public interface IPersonClaimBl
{
    IDataResult<PersonClaimDto> Add(PersonClaimDto personClaimDto);
    IResult Delete(long id);
    IDataResult<PersonClaimDto> GetById(long id);
    IDataResult<List<PersonClaimExtDto>> GetExtsByPersonId(long personId);
}
