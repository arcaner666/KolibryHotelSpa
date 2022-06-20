using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Dapper;

namespace DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsCurrencyDal : ICurrencyDal
{
    private readonly DapperContext _context;

    public DpMsCurrencyDal(DapperContext context)
    {
        _context = context;
    }

    public List<Currency> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " CurrencyId,"
            + " Title,"
            + " CurrencySymbol,"
            + " ExchangeRate"
            + " FROM Currency";
        return connection.Query<Currency>(sql).ToList();
    }        
    
    public Currency GetByTitle(string title)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " CurrencyId,"
            + " Title,"
            + " CurrencySymbol,"
            + " ExchangeRate"
            + " FROM Currency"
            + " WHERE Title = @Title";
        return connection.Query<Currency>(sql, new { @Title = title }).SingleOrDefault();
    }
}
