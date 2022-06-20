using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface ICurrencyBl
{
    IDataResult<List<CurrencyDto>> GetAll();
    IDataResult<CurrencyDto> GetByTitle(string title);
}
