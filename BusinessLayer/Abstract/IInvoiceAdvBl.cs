using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface IInvoiceAdvBl
{
    IResult Add(InvoiceExtDto invoiceExtDto);
    IResult Delete(long id);
    IResult Update(InvoiceExtDto invoiceExtDto);
}
