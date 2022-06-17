using Entities.DatabaseModels;
using Entities.ExtendedDatabaseModels;

namespace DataAccessLayer.Abstract;

public interface IInvoiceDal
{
    long Add(Invoice invoice);
    void Delete(long id);
    Invoice GetById(long id);
    List<InvoiceExt> GetExts();
    void Update(Invoice invoice);
}
