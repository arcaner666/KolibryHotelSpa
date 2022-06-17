using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Dapper;

namespace DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsSuiteDal : ISuiteDal
{
    private readonly DapperContext _context;

    public DpMsSuiteDal(DapperContext context)
    {
        _context = context;
    }

    public int Add(Suite suite)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Suite ("
            + " Title,"
            + " Bed,"
            + " M2,"
            + " Price,"
            + " Vat,"
            + " TotalVat,"
            + " TotalPrice,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @Title,"
            + " @Bed,"
            + " @M2,"
            + " @Price,"
            + " @Vat,"
            + " @TotalVat,"
            + " @TotalPrice,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS INT)";
        return connection.Query<int>(sql, suite).Single();
    }

    public void Delete(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Suite"
            + " WHERE SuiteId = @SuiteId";
        connection.Execute(sql, new { @SuiteId = id });
    }

    public List<Suite> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " SuiteId,"
            + " Title,"
            + " Bed,"
            + " M2,"
            + " Price,"
            + " Vat,"
            + " TotalVat,"
            + " TotalPrice,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Suite";
        return connection.Query<Suite>(sql).ToList();
    }

    public Suite GetById(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " SuiteId,"
            + " Title,"
            + " Bed,"
            + " M2,"
            + " Price,"
            + " Vat,"
            + " TotalVat,"
            + " TotalPrice,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Suite"
            + " WHERE SuiteId = @SuiteId";
        return connection.Query<Suite>(sql, new { @SuiteId = id }).SingleOrDefault();
    }

    public Suite GetByTitle(string title)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " SuiteId,"
            + " Title,"
            + " Bed,"
            + " M2,"
            + " Price,"
            + " Vat,"
            + " TotalVat,"
            + " TotalPrice,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Suite"
            + " WHERE Title = @Title";
        return connection.Query<Suite>(sql, new { @Title = title }).SingleOrDefault();
    }

    public void Update(Suite suite)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Suite SET"
            + " Title = @Title,"
            + " Bed = @Bed,"
            + " M2 = @M2,"
            + " Price = @Price,"
            + " Vat = @Vat,"
            + " TotalVat = @TotalVat,"
            + " TotalPrice = @TotalPrice,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE SuiteId = @SuiteId";
        connection.Execute(sql, suite);
    }
}
