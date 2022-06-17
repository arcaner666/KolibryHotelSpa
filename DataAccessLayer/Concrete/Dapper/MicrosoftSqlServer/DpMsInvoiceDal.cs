using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Entities.ExtendedDatabaseModels;
using Dapper;

namespace DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsInvoiceDal : IInvoiceDal
{
    private readonly DapperContext _context;

    public DpMsInvoiceDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(Invoice invoice)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Invoice ("
            + " CurrencyId,"
            + " BuyerNameSurname,"
            + " BuyerEmail,"
            + " BuyerPhone,"
            + " ReservationStartDate,"
            + " ReservationEndDate,"
            + " Adult,"
            + " Child,"
            + " ChildAge1,"
            + " ChildAge2,"
            + " ChildAge3,"
            + " ChildAge4,"
            + " ChildAge5,"
            + " ChildAge6,"
            + " Title,"
            + " NetPrice,"
            + " Vat,"
            + " TotalVat,"
            + " TotalPrice,"
            + " Paid,"
            + " Canceled,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @CurrencyId,"
            + " @BuyerNameSurname,"
            + " @BuyerEmail,"
            + " @BuyerPhone,"
            + " @ReservationStartDate,"
            + " @ReservationEndDate,"
            + " @Adult,"
            + " @Child,"
            + " @ChildAge1,"
            + " @ChildAge2,"
            + " @ChildAge3,"
            + " @ChildAge4,"
            + " @ChildAge5,"
            + " @ChildAge6,"
            + " @Title,"
            + " @NetPrice,"
            + " @Vat,"
            + " @TotalVat,"
            + " @TotalPrice,"
            + " @Paid,"
            + " @Canceled,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return connection.Query<long>(sql, invoice).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Invoice"
            + " WHERE InvoiceId = @InvoiceId";
        connection.Execute(sql, new { @InvoiceId = id });
    }

    public Invoice GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " InvoiceId,"
            + " CurrencyId,"
            + " BuyerNameSurname,"
            + " BuyerEmail,"
            + " BuyerPhone,"
            + " ReservationStartDate,"
            + " ReservationEndDate,"
            + " Adult,"
            + " Child,"
            + " ChildAge1,"
            + " ChildAge2,"
            + " ChildAge3,"
            + " ChildAge4,"
            + " ChildAge5,"
            + " ChildAge6,"
            + " Title,"
            + " NetPrice,"
            + " Vat,"
            + " TotalVat,"
            + " TotalPrice,"
            + " Paid,"
            + " Canceled,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Invoice"
            + " WHERE InvoiceId = @InvoiceId";
        return connection.Query<Invoice>(sql, new { @InvoiceId = id }).SingleOrDefault();
    }

    public List<InvoiceExt> GetExts()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " InvoiceId,"
            + " CurrencyId,"
            + " BuyerNameSurname,"
            + " BuyerEmail,"
            + " BuyerPhone,"
            + " ReservationStartDate,"
            + " ReservationEndDate,"
            + " Adult,"
            + " Child,"
            + " ChildAge1,"
            + " ChildAge2,"
            + " ChildAge3,"
            + " ChildAge4,"
            + " ChildAge5,"
            + " ChildAge6,"
            + " Title,"
            + " NetPrice,"
            + " Vat,"
            + " TotalVat,"
            + " TotalPrice,"
            + " Paid,"
            + " Canceled,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Invoice";
        return connection.Query<InvoiceExt>(sql).ToList();
    }

    public void Update(Invoice invoice)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Invoice SET"
            + " CurrencyId = @CurrencyId,"
            + " BuyerNameSurname = @BuyerNameSurname,"
            + " BuyerEmail = @BuyerEmail,"
            + " BuyerPhone = @BuyerPhone,"
            + " ReservationStartDate = @ReservationStartDate,"
            + " ReservationEndDate = @ReservationEndDate,"
            + " Adult = @Adult,"
            + " Child = @Child,"
            + " ChildAge1 = @ChildAge1,"
            + " ChildAge2 = @ChildAge2,"
            + " ChildAge3 = @ChildAge3,"
            + " ChildAge4 = @ChildAge4,"
            + " ChildAge5 = @ChildAge5,"
            + " ChildAge6 = @ChildAge6,"
            + " Title = @Title,"
            + " NetPrice = @NetPrice,"
            + " Vat = @Vat,"
            + " TotalVat = @TotalVat,"
            + " TotalPrice = @TotalPrice,"
            + " Paid = @Paid,"
            + " Canceled = @Canceled,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE InvoiceId = @InvoiceId";
        connection.Execute(sql, invoice);
    }
}
