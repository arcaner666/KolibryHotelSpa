using Entities.DatabaseModels;

namespace DataAccessLayer.Abstract;

public interface IClaimDal
{
    List<Claim> GetAll();
    Claim GetByTitle(string title);
}
