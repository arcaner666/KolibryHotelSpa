using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface IContactFormBl
{
    IDataResult<ContactFormDto> Add(ContactFormDto contactFormDto);
    IResult Delete(long id);
    IDataResult<List<ContactFormDto>> GetAll();
    IDataResult<ContactFormDto> GetById(long id);
}
