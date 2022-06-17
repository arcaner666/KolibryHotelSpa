using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Aspects.Autofac.Validation;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Entities.DTOs;
using Entities.ExtendedDatabaseModels;

namespace BusinessLayer.Concrete;

public class InvoiceDetailBl : IInvoiceDetailBl
{
    private readonly IMapper _mapper;
    private readonly IInvoiceDetailDal _invoiceDetailDal;

    public InvoiceDetailBl(
        IMapper mapper,
        IInvoiceDetailDal invoiceDetailDal
    )
    {
        _mapper = mapper;
        _invoiceDetailDal = invoiceDetailDal;
    }

    public IDataResult<InvoiceDetailDto> Add(InvoiceDetailDto invoiceDetailDto)
    {
        InvoiceDetail searchedInvoiceDetail = _invoiceDetailDal.GetByInvoiceIdAndSuiteId(invoiceDetailDto.InvoiceId, invoiceDetailDto.SuiteId);
        if (searchedInvoiceDetail is not null)
            return new ErrorDataResult<InvoiceDetailDto>(Messages.InvoiceDetailAlreadyExists);

        var addedInvoiceDetail = _mapper.Map<InvoiceDetail>(invoiceDetailDto);

        addedInvoiceDetail.TotalVat = CalculateTotalVat(addedInvoiceDetail.Price, addedInvoiceDetail.Vat);
        addedInvoiceDetail.TotalPrice = addedInvoiceDetail.Price + addedInvoiceDetail.TotalVat;
        long id = _invoiceDetailDal.Add(addedInvoiceDetail);
        addedInvoiceDetail.InvoiceDetailId = id;

        var addedInvoiceDetailDto = _mapper.Map<InvoiceDetailDto>(addedInvoiceDetail);

        return new SuccessDataResult<InvoiceDetailDto>(addedInvoiceDetailDto, Messages.InvoiceDetailAdded);
    }

    public IResult Delete(long id)
    {
        var getInvoiceDetailResult = GetById(id);
        if (getInvoiceDetailResult is null)
            return getInvoiceDetailResult;

        _invoiceDetailDal.Delete(id);

        return new SuccessResult(Messages.InvoiceDetailDeleted);
    }

    public IDataResult<InvoiceDetailDto> GetById(long id)
    {
        InvoiceDetail invoiceDetail = _invoiceDetailDal.GetById(id);
        if (invoiceDetail is null)
            return new ErrorDataResult<InvoiceDetailDto>(Messages.InvoiceDetailNotFound);

        var invoiceDetailDto = _mapper.Map<InvoiceDetailDto>(invoiceDetail);

        return new SuccessDataResult<InvoiceDetailDto>(invoiceDetailDto, Messages.InvoiceDetailListedById);
    }

    public IDataResult<List<InvoiceDetailDto>> GetByInvoiceId(long invoiceId)
    {
        List<InvoiceDetail> invoiceDetails = _invoiceDetailDal.GetByInvoiceId(invoiceId);
        if (!invoiceDetails.Any())
            return new ErrorDataResult<List<InvoiceDetailDto>>(Messages.InvoiceDetailsNotFound);

        var invoiceDetailDtos = _mapper.Map<List<InvoiceDetailDto>>(invoiceDetails);

        return new SuccessDataResult<List<InvoiceDetailDto>>(invoiceDetailDtos, Messages.InvoiceDetailsListedByInvoiceId);
    }

    public IResult Update(InvoiceDetailDto invoiceDetailDto)
    {
        InvoiceDetail invoiceDetail = _invoiceDetailDal.GetById(invoiceDetailDto.InvoiceDetailId);
        if (invoiceDetail is null)
            return new ErrorDataResult<PersonDto>(Messages.InvoiceDetailNotFound);

        invoiceDetail.SuiteId = invoiceDetailDto.SuiteId;
        invoiceDetail.Amount = invoiceDetailDto.Amount;
        invoiceDetail.Price = invoiceDetailDto.Price;
        invoiceDetail.Vat = invoiceDetailDto.Vat;
        invoiceDetail.TotalVat = CalculateTotalVat(invoiceDetailDto.Price, invoiceDetailDto.Vat);
        invoiceDetail.TotalPrice = invoiceDetailDto.Price + invoiceDetail.TotalVat;
        _invoiceDetailDal.Update(invoiceDetail);

        return new SuccessResult(Messages.InvoiceDetailUpdated);
    }

    private decimal CalculateTotalVat(decimal price, decimal vat)
    {
        return price * vat / 100;
    }
}
