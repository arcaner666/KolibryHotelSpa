using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface IPersonBl
{
    IDataResult<PersonDto> Add(PersonDto personDto);
    IResult Delete(long id);
    IDataResult<PersonDto> GetByEmail(string email);
    IDataResult<PersonDto> GetById(long id);
    IDataResult<PersonDto> GetByPhone(string phone);
    IDataResult<List<PersonExtDto>> GetExts();
    IResult Update(PersonDto personDto);
}
