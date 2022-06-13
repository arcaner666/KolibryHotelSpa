using Entities.DatabaseModels;

namespace DataAccessLayer.Abstract;

public interface IPersonDal
{
    long Add(Person person);
    void Delete(long id);
    Person GetByEmail(string email);
    Person GetById(long id);
    Person GetByPhone(string phone);
    void Update(Person person);
}
