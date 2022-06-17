using Entities.DatabaseModels;
using Entities.ExtendedDatabaseModels;

namespace DataAccessLayer.Abstract;

public interface IInvoiceDetailDal
{
    long Add(InvoiceDetail invoiceDetail);
    void Delete(long id);
    InvoiceDetail GetById(long id);
    List<InvoiceDetail> GetByInvoiceId(long invoiceId);
    InvoiceDetail GetByInvoiceIdAndSuiteId(long invoiceId, int suiteId);
    void Update(InvoiceDetail invoiceDetail);
}
