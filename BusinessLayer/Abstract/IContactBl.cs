using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface IContactBl
{
    IDataResult<ContactDto> Add(ContactDto contactDto);
    IResult Delete(long id);
    IDataResult<List<ContactDto>> GetAll();
    IDataResult<ContactDto> GetById(long id);
}
