using Entities.DatabaseModels;

namespace DataAccessLayer.Abstract;

public interface ICurrencyDal
{
    List<Currency> GetAll();
    Currency GetByCurrencyName(string currencyName);
}
