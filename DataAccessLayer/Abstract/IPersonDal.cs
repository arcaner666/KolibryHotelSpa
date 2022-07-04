using Entities.DatabaseModels;

namespace DataAccessLayer.Abstract;

public interface IPersonDal
{
    long Add(Person person);
    void Delete(long id);
    Person GetByEmail(string email);
    Person GetByEmailOrPhone(string email, string phone);
    Person GetById(long id);
    Person GetByPhone(string phone);
    List<PersonExt> GetExts();
    void Update(Person person);
}
