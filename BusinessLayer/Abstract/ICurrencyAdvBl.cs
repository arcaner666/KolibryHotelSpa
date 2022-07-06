using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface ICurrencyAdvBl
{
    IDataResult<List<CurrencyDto>> GetAll();
    IResult TestLogAspect(int number, string str);
}
