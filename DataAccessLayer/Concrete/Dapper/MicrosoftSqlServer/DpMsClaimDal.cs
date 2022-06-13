using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Dapper;

namespace DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsClaimDal : IClaimDal
{
    private readonly DapperContext _context;

    public DpMsClaimDal(DapperContext context)
    {
        _context = context;
    }

    public List<Claim> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " c.ClaimId,"
            + " c.Title"
            + " FROM Claim c";
        return connection.Query<Claim>(sql).ToList();
    }

    public Claim GetByTitle(string title)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
           + " c.ClaimId,"
           + " c.Title"
           + " FROM Claim c"
           + " WHERE c.Title = @Title";
        return connection.Query<Claim>(sql, new { @Title = title }).SingleOrDefault();
    }
}
