using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Dapper;

namespace DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsPersonDal : IPersonDal
{
    private readonly DapperContext _context;

    public DpMsPersonDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(Person person)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Person ("
            + " UserName,"
            + " Email,"
            + " Phone,"
            + " PasswordHash,"
            + " PasswordSalt,"
            + " Role,"
            + " Blocked,"
            + " RefreshToken,"
            + " RefreshTokenExpiryTime,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @UserName,"
            + " @Email,"
            + " @Phone,"
            + " @PasswordHash,"
            + " @PasswordSalt,"
            + " @Role,"
            + " @Blocked,"
            + " @RefreshToken,"
            + " @RefreshTokenExpiryTime,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return connection.Query<long>(sql, person).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Person"
            + " WHERE PersonId = @PersonId";
        connection.Execute(sql, new { @PersonId = id });
    }

    public Person GetByEmail(string email)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " PersonId,"
            + " UserName,"
            + " Email,"
            + " Phone,"
            + " PasswordHash,"
            + " PasswordSalt,"
            + " Role,"
            + " Blocked,"
            + " RefreshToken,"
            + " RefreshTokenExpiryTime,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Person"
            + " WHERE Email = @Email";
        return connection.Query<Person>(sql, new { @Email = email }).SingleOrDefault();
    }

    public Person GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " PersonId,"
            + " UserName,"
            + " Email,"
            + " Phone,"
            + " PasswordHash,"
            + " PasswordSalt,"
            + " Role,"
            + " Blocked,"
            + " RefreshToken,"
            + " RefreshTokenExpiryTime,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Person"
            + " WHERE PersonId = @PersonId";
        return connection.Query<Person>(sql, new { @PersonId = id }).SingleOrDefault();
    }

    public Person GetByPhone(string phone)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " PersonId,"
            + " UserName,"
            + " Email,"
            + " Phone,"
            + " PasswordHash,"
            + " PasswordSalt,"
            + " Role,"
            + " Blocked,"
            + " RefreshToken,"
            + " RefreshTokenExpiryTime,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Person"
            + " WHERE Phone = @Phone";
        return connection.Query<Person>(sql, new { @Phone = phone }).SingleOrDefault();
    }

    public void Update(Person person)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Person SET"
            + " UserName = @UserName,"
            + " Email = @Email,"
            + " Phone = @Phone,"
            + " PasswordHash = @PasswordHash,"
            + " PasswordSalt = @PasswordSalt,"
            + " Role = @Role,"
            + " Blocked = @Blocked,"
            + " RefreshToken = @RefreshToken,"
            + " RefreshTokenExpiryTime = @RefreshTokenExpiryTime,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE PersonId = @PersonId";
        connection.Execute(sql, person);
    }
}
