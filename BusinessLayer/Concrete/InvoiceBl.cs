using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Entities.DTOs;
using Entities.ExtendedDatabaseModels;

namespace BusinessLayer.Concrete;

public class InvoiceBl : IInvoiceBl
{
    private readonly IInvoiceDal _invoiceDal;
    private readonly IMapper _mapper;

    public InvoiceBl(
        IInvoiceDal invoiceDal,
        IMapper mapper
    )
    {
        _invoiceDal = invoiceDal;
        _mapper = mapper;
    }

    public IDataResult<InvoiceDto> Add(InvoiceDto invoiceDto)
    {
        Invoice searchedInvoice = _invoiceDal.GetIfAlreadyExist(
            invoiceDto.CurrencyId,
            invoiceDto.BuyerNameSurname,
            invoiceDto.BuyerEmail,
            invoiceDto.BuyerPhone,
            invoiceDto.BuyerAddress,
            invoiceDto.BuyerIp,
            invoiceDto.ReservationStartDate
        );
        if (searchedInvoice is not null)
            return new ErrorDataResult<InvoiceDto>(Messages.InvoiceAlreadyExists);

        var addedInvoice = _mapper.Map<Invoice>(invoiceDto);

        addedInvoice.Paid = false;
        addedInvoice.Canceled = false;
        addedInvoice.CreatedAt = DateTimeOffset.Now;
        addedInvoice.UpdatedAt = DateTimeOffset.Now;
        long id = _invoiceDal.Add(addedInvoice);
        addedInvoice.InvoiceId = id;

        var addedInvoiceDto = _mapper.Map<InvoiceDto>(addedInvoice);

        return new SuccessDataResult<InvoiceDto>(addedInvoiceDto, Messages.InvoiceAdded);
    }

    public IResult Delete(long id)
    {
        var getInvoiceResult = GetById(id);
        if (getInvoiceResult is null)
            return getInvoiceResult;

        _invoiceDal.Delete(id);

        return new SuccessResult(Messages.InvoiceDeleted);
    }

    public IDataResult<InvoiceDto> GetById(long id)
    {
        Invoice invoice = _invoiceDal.GetById(id);
        if (invoice is null)
            return new ErrorDataResult<InvoiceDto>(Messages.InvoiceNotFound);

        var invoiceDto = _mapper.Map<InvoiceDto>(invoice);

        return new SuccessDataResult<InvoiceDto>(invoiceDto, Messages.InvoiceListedById);
    }

    public IDataResult<List<InvoiceExtDto>> GetExts()
    {
        List<InvoiceExt> invoiceExts = _invoiceDal.GetExts();
        if (!invoiceExts.Any())
            return new ErrorDataResult<List<InvoiceExtDto>>(Messages.InvoicesNotFound);

        var invoiceExtDtos = _mapper.Map<List<InvoiceExtDto>>(invoiceExts);

        return new SuccessDataResult<List<InvoiceExtDto>>(invoiceExtDtos, Messages.InvoiceExtsListed);
    }

    public IResult Update(InvoiceDto invoiceDto)
    {
        Invoice invoice = _invoiceDal.GetById(invoiceDto.InvoiceId);
        if (invoice is null)
            return new ErrorResult(Messages.InvoiceNotFound);

        invoice.Paid = invoiceDto.Paid;
        invoice.Canceled = invoiceDto.Canceled;
        invoice.UpdatedAt = DateTimeOffset.Now;
        _invoiceDal.Update(invoice);

        return new SuccessResult(Messages.InvoiceUpdated);
    }
}
