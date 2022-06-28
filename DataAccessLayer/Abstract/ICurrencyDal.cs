using Entities.DatabaseModels;

namespace DataAccessLayer.Abstract;

public interface ICurrencyDal
{
    List<Currency> GetAll();
    Currency GetById(byte id);
    Currency GetByTitle(string title);
    void Update(Currency currency);
}
