using Entities.DatabaseModels;
using Entities.ExtendedDatabaseModels;

namespace DataAccessLayer.Abstract;

public interface IInvoiceDal
{
    long Add(Invoice invoice);
    void Delete(long id);
    Invoice GetById(long id);
    List<InvoiceExt> GetExts();
    Invoice GetIfAlreadyExist(
        byte currencyId,
        string buyerNameSurname,
        string buyerEmail,
        string buyerPhone,
        string buyerAddress,
        string buyerIp,
        DateTimeOffset reservationStartDate
    );
    void Update(Invoice invoice);
}
