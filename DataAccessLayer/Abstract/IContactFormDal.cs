using Entities.DatabaseModels;

namespace DataAccessLayer.Abstract;

public interface IContactFormDal
{
    long Add(ContactForm contactForm);
    void Delete(long id);
    List<ContactForm> GetAll();
    ContactForm GetById(long id);
    ContactForm GetByMessage(string message);
    void Update(ContactForm contactForm);
}
