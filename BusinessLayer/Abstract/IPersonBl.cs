using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface IPersonBl
{
    IDataResult<PersonDto> Add(PersonDto personDto);
    IDataResult<PersonDto> GetByEmail(string email);
    IDataResult<PersonDto> GetById(long id);
    IDataResult<PersonDto> GetByPhone(string phone);
    IResult Update(PersonDto personDto);
}
