using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface IInvoiceDetailBl
{
    IDataResult<InvoiceDetailDto> Add(InvoiceDetailDto invoiceDetailDto);
    IResult Delete(long id);
    IDataResult<InvoiceDetailDto> GetById(long id);
    IDataResult<List<InvoiceDetailDto>> GetByInvoiceId(long invoiceId);
    IResult Update(InvoiceDetailDto invoiceDetailDto);
}
