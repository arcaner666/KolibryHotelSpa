using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface ICurrencyAdvBl
{
    IResult UpdateExchangeRates();
}
