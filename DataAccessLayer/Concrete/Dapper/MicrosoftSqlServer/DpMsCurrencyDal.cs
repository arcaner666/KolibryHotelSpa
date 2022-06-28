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
            + " AlphabeticCode,"
            + " CurrencySymbol,"
            + " ExchangeRate,"
            + " UpdatedAt"
            + " FROM Currency";
        return connection.Query<Currency>(sql).ToList();
    }

    public Currency GetById(byte id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " CurrencyId,"
            + " Title,"
            + " AlphabeticCode,"
            + " CurrencySymbol,"
            + " ExchangeRate,"
            + " UpdatedAt"
            + " FROM Currency"
            + " WHERE CurrencyId = @CurrencyId";
        return connection.Query<Currency>(sql, new { @CurrencyId = id }).SingleOrDefault();
    }

    public Currency GetByTitle(string title)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " CurrencyId,"
            + " Title,"
            + " AlphabeticCode,"
            + " CurrencySymbol,"
            + " ExchangeRate,"
            + " UpdatedAt"
            + " FROM Currency"
            + " WHERE Title = @Title";
        return connection.Query<Currency>(sql, new { @Title = title }).SingleOrDefault();
    }

    public void Update(Currency currency)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Currency SET"
            + " Title = @Title,"
            + " AlphabeticCode = @AlphabeticCode,"
            + " CurrencySymbol = @CurrencySymbol,"
            + " ExchangeRate = @ExchangeRate,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE CurrencyId = @CurrencyId";
        connection.Execute(sql, currency);
    }
}
