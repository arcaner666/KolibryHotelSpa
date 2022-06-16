using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Dapper;

namespace DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsContactDal : IContactDal
{
    private readonly DapperContext _context;

    public DpMsContactDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(Contact contact)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Contact ("
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " Message,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @NameSurname,"
            + " @Email,"
            + " @Phone,"
            + " @Message,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return connection.Query<long>(sql, contact).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Contact"
            + " WHERE ContactId = @ContactId";
        connection.Execute(sql, new { @ContactId = id });
    }

    public List<Contact> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " ContactId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " Message,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Contact";
        return connection.Query<Contact>(sql).ToList();
    }

    public Contact GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " ContactId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " Message,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Contact"
            + " WHERE ContactId = @ContactId";
        return connection.Query<Contact>(sql, new { @ContactId = id }).SingleOrDefault();
    }

    public Contact GetByMessage(string message)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " ContactId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " Message,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Contact"
            + " WHERE Message = @Message";
        return connection.Query<Contact>(sql, new { @Message = message }).SingleOrDefault();
    }

    public void Update(Contact contact)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Contact SET"
            + " NameSurname = @NameSurname,"
            + " Email = @Email,"
            + " Phone = @Phone,"
            + " Message = @Message,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE ContactId = @ContactId";
        connection.Execute(sql, contact);
    }
}
