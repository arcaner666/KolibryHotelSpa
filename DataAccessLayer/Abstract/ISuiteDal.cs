using Entities.DatabaseModels;

namespace DataAccessLayer.Abstract;

public interface ISuiteDal
{
    int Add(Suite suite);
    void Delete(int id);
    List<Suite> GetAll();
    Suite GetById(int id);
    Suite GetByTitle(string title);
    void Update(Suite suite);
}
