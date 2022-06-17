using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Dapper;

namespace DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsContactFormDal : IContactFormDal
{
    private readonly DapperContext _context;

    public DpMsContactFormDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(ContactForm contactForm)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO ContactForm ("
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
        return connection.Query<long>(sql, contactForm).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM ContactForm"
            + " WHERE ContactFormId = @ContactFormId";
        connection.Execute(sql, new { @ContactFormId = id });
    }

    public List<ContactForm> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " ContactFormId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " Message,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM ContactForm";
        return connection.Query<ContactForm>(sql).ToList();
    }

    public ContactForm GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " ContactFormId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " Message,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM ContactForm"
            + " WHERE ContactFormId = @ContactFormId";
        return connection.Query<ContactForm>(sql, new { @ContactFormId = id }).SingleOrDefault();
    }

    public ContactForm GetByMessage(string message)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " ContactFormId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " Message,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM ContactForm"
            + " WHERE Message = @Message";
        return connection.Query<ContactForm>(sql, new { @Message = message }).SingleOrDefault();
    }

    public void Update(ContactForm contactForm)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE ContactForm SET"
            + " NameSurname = @NameSurname,"
            + " Email = @Email,"
            + " Phone = @Phone,"
            + " Message = @Message,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE ContactFormId = @ContactFormId";
        connection.Execute(sql, contactForm);
    }
}
