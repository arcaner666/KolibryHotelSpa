using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Dapper;
using Entities.ExtendedDatabaseModels;

namespace DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsInvoiceDetailDal : IInvoiceDetailDal
{
    private readonly DapperContext _context;

    public DpMsInvoiceDetailDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(InvoiceDetail invoiceDetail)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO InvoiceDetail ("
            + " InvoiceId,"
            + " SuiteId,"
            + " Amount,"
            + " Price,"
            + " Vat,"
            + " TotalVat,"
            + " TotalPrice)"
            + " VALUES("
            + " @InvoiceId,"
            + " @SuiteId,"
            + " @Amount,"
            + " @Price,"
            + " @Vat,"
            + " @TotalVat,"
            + " @TotalPrice)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return connection.Query<long>(sql, invoiceDetail).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM InvoiceDetail"
            + " WHERE InvoiceDetailId = @InvoiceDetailId";
        connection.Execute(sql, new { @InvoiceDetailId = id });
    }

    public InvoiceDetail GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " InvoiceDetailId,"
            + " InvoiceId,"
            + " SuiteId,"
            + " Amount,"
            + " Price,"
            + " Vat,"
            + " TotalVat,"
            + " TotalPrice"
            + " FROM InvoiceDetail"
            + " WHERE InvoiceDetailId = @InvoiceDetailId";
        return connection.Query<InvoiceDetail>(sql, new { @InvoiceDetailId = id }).SingleOrDefault();
    }

    public List<InvoiceDetail> GetByInvoiceId(long invoiceId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " InvoiceDetailId,"
            + " InvoiceId,"
            + " SuiteId,"
            + " Amount,"
            + " Price,"
            + " Vat,"
            + " TotalVat,"
            + " TotalPrice"
            + " FROM InvoiceDetail"
            + " WHERE InvoiceId = @InvoiceId";
        return connection.Query<InvoiceDetail>(sql, new { @InvoiceId = invoiceId }).ToList();
    }

    public InvoiceDetail GetByInvoiceIdAndSuiteId(long invoiceId, int suiteId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " InvoiceDetailId,"
            + " InvoiceId,"
            + " SuiteId,"
            + " Amount,"
            + " Price,"
            + " Vat,"
            + " TotalVat,"
            + " TotalPrice"
            + " FROM InvoiceDetail"
            + " WHERE InvoiceId = @InvoiceId"
            + " AND SuiteId = @SuiteId";
        return connection.Query<InvoiceDetail>(sql, new 
        {
            @InvoiceId = invoiceId,
            @SuiteId = suiteId,
        }).SingleOrDefault();
    }

    public void Update(InvoiceDetail invoiceDetail)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE InvoiceDetail SET"
            + " InvoiceId = @InvoiceId,"
            + " SuiteId = @SuiteId,"
            + " Amount = @Amount,"
            + " Price = @Price,"
            + " Vat = @Vat,"
            + " TotalVat = @TotalVat,"
            + " TotalPrice = @TotalPrice"
            + " WHERE InvoiceDetailId = @InvoiceDetailId";
        connection.Execute(sql, invoiceDetail);
    }
}
