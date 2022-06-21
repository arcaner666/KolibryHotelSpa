using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Aspects.Autofac.Transaction;
using BusinessLayer.Aspects.Autofac.Validation;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using BusinessLayer.ValidationRules.FluentValidation;
using Entities.DTOs;

namespace BusinessLayer.Concrete;

public class InvoiceAdvBl : IInvoiceAdvBl
{
    private readonly IInvoiceBl _invoiceBl;
    private readonly IInvoiceDetailBl _invoiceDetailBl;
    private readonly IMapper _mapper;

    public InvoiceAdvBl(
        IInvoiceBl invoiceBl,
        IInvoiceDetailBl invoiceDetailBl,
        IMapper mapper
    )
    {
        _invoiceBl = invoiceBl;
        _invoiceDetailBl = invoiceDetailBl;
        _mapper = mapper;
    }

    [TransactionScopeAspect]
    [ValidationAspect(typeof(InvoiceExtDtoForAddValidator))]
    public IDataResult<InvoiceExtDto> Add(InvoiceExtDto invoiceExtDto)
    {
        var invoiceDto = _mapper.Map<InvoiceDto>(invoiceExtDto);

        var addedInvoice = _invoiceBl.Add(invoiceDto);
        if (!addedInvoice.Success)
            return new ErrorDataResult<InvoiceExtDto>(addedInvoice.Message);

        foreach (var invoiceDetailDto in invoiceExtDto.InvoiceDetailDtos)
        {
            invoiceDetailDto.InvoiceId = addedInvoice.Data.InvoiceId;
            var addInvoiceDetailResult = _invoiceDetailBl.Add(invoiceDetailDto);
            if (!addInvoiceDetailResult.Success)
                return new ErrorDataResult<InvoiceExtDto>(addInvoiceDetailResult.Message);
        }

        var responseInvoiceExtDto = _mapper.Map<InvoiceExtDto>(addedInvoice.Data);

        return new SuccessDataResult<InvoiceExtDto>(responseInvoiceExtDto, Messages.InvoiceExtAdded);
    }

    [TransactionScopeAspect]
    public IResult Delete(long id)
    {
        var getInvoiceDetailsResult = _invoiceDetailBl.GetByInvoiceId(id);
        if (!getInvoiceDetailsResult.Success)
            return getInvoiceDetailsResult;

        foreach (var invoiceDetailDto in getInvoiceDetailsResult.Data)
        {
            var deleteInvoiceDetailResult = _invoiceDetailBl.Delete(invoiceDetailDto.InvoiceDetailId);
            if (!deleteInvoiceDetailResult.Success)
                return deleteInvoiceDetailResult;
        }

        var deleteInvoiceResult = _invoiceBl.Delete(id);
        if (!deleteInvoiceResult.Success)
            return deleteInvoiceResult;

        return new SuccessResult(Messages.InvoiceExtDeleted);
    }

    [TransactionScopeAspect]
    public IResult Update(InvoiceExtDto invoiceExtDto)
    {
        var invoiceDto = _mapper.Map<InvoiceDto>(invoiceExtDto);

        var updateInvoiceResult = _invoiceBl.Update(invoiceDto);
        if (!updateInvoiceResult.Success)
            return updateInvoiceResult;

        foreach (var invoiceDetailDto in invoiceExtDto.InvoiceDetailDtos)
        {
            var updateInvoiceDetailResult = _invoiceDetailBl.Update(invoiceDetailDto);
            if (!updateInvoiceDetailResult.Success)
                return updateInvoiceDetailResult;
        }

        return new SuccessResult(Messages.InvoiceExtUpdated);
    }
}
