using Entities.DatabaseModels;
using Entities.ExtendedDatabaseModels;

namespace DataAccessLayer.Abstract;

public interface IPersonClaimDal
{
    long Add(PersonClaim personClaim);
    void Delete(long id);
    PersonClaim GetById(long id);
    PersonClaim GetByPersonIdAndClaimId(long personId, int claimId);
    List<PersonClaimExt> GetExtsByPersonId(long personId);
}
