using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Entities.ExtendedDatabaseModels;
using Dapper;

namespace DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsPersonClaimDal : IPersonClaimDal
{
    private readonly DapperContext _context;

    public DpMsPersonClaimDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(PersonClaim personClaim)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO PersonClaim ("
            + " PersonId,"
            + " ClaimId,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @PersonId,"
            + " @ClaimId,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return connection.Query<long>(sql, personClaim).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM PersonClaim"
            + " WHERE PersonClaimId = @PersonClaimId";
        connection.Execute(sql, new { @PersonClaimId = id });
    }

    public PersonClaim GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " PersonClaimId,"
            + " PersonId,"
            + " ClaimId,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM PersonClaim"
            + " WHERE PersonClaimId = @PersonClaimId";
        return connection.Query<PersonClaim>(sql, new { @PersonClaimId = id }).SingleOrDefault();
    }

    public PersonClaim GetByPersonIdAndClaimId(long personId, int claimId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " PersonClaimId,"
            + " PersonId,"
            + " ClaimId,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM PersonClaim"
            + " WHERE PersonId = @PersonId AND ClaimId = @ClaimId";
        return connection.Query<PersonClaim>(sql, new
        {
            @PersonId = personId,
            @ClaimId = claimId
        }).SingleOrDefault();
    }

    public List<PersonClaimExt> GetExtsByPersonId(long personId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " pc.PersonClaimId,"
            + " pc.PersonId,"
            + " pc.ClaimId,"
            + " pc.CreatedAt,"
            + " pc.UpdatedAt,"
            + " c.Title AS ClaimTitle"
            + " FROM PersonClaim pc"
            + " INNER JOIN Claim c ON pc.ClaimId = c.ClaimId"
            + " WHERE pc.PersonId = @PersonId";
        return connection.Query<PersonClaimExt>(sql, new { @PersonId = personId }).ToList();
    }
}
