using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface IInvoiceBl
{
    IDataResult<InvoiceDto> Add(InvoiceDto invoiceDto);
    IResult Delete(long id);
    IDataResult<InvoiceDto> GetById(long id);
    IDataResult<List<InvoiceExtDto>> GetExts();
    IResult Update(InvoiceDto invoiceDto);
}
