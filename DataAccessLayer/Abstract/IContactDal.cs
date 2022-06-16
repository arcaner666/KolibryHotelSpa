using Entities.DatabaseModels;

namespace DataAccessLayer.Abstract;

public interface IContactDal
{
    long Add(Contact contact);
    void Delete(long id);
    List<Contact> GetAll();
    Contact GetById(long id);
    Contact GetByMessage(string message);
    void Update(Contact contact);
}
