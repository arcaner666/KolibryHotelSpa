using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Aspects.Autofac.Validation;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Entities.DTOs;

namespace BusinessLayer.Concrete;

public class SuiteBl : ISuiteBl
{
    private readonly IMapper _mapper;
    private readonly ISuiteDal _suiteDal;

    public SuiteBl(
        IMapper mapper,
        ISuiteDal suiteDal
    )
    {
        _mapper = mapper;
        _suiteDal = suiteDal;
    }

    [ValidationAspect(typeof(SuiteDtoForAddValidator))]
    public IDataResult<SuiteDto> Add(SuiteDto suiteDto)
    {
        Suite searchedSuite = _suiteDal.GetByTitle(suiteDto.Title);
        if (searchedSuite is not null)
            return new ErrorDataResult<SuiteDto>(Messages.SuiteAlreadyExists);

        var addedSuite = _mapper.Map<Suite>(suiteDto);

        addedSuite.TotalVat = CalculateTotalVat(addedSuite.Price, addedSuite.Vat);
        addedSuite.TotalPrice = addedSuite.Price + addedSuite.TotalVat;
        addedSuite.CreatedAt = DateTimeOffset.Now;
        addedSuite.UpdatedAt = DateTimeOffset.Now;
        int id = _suiteDal.Add(addedSuite);
        addedSuite.SuiteId = id;

        var addedSuiteDto = _mapper.Map<SuiteDto>(addedSuite);

        return new SuccessDataResult<SuiteDto>(addedSuiteDto, Messages.SuiteAdded);
    }

    public IResult Delete(int id)
    {
        var getSuiteResult = GetById(id);
        if (getSuiteResult is null)
            return getSuiteResult;

        _suiteDal.Delete(id);

        return new SuccessResult(Messages.SuiteDeleted);
    }

    public IDataResult<List<SuiteDto>> GetAll()
    {
        List<Suite> suites = _suiteDal.GetAll();
        if (!suites.Any())
            return new ErrorDataResult<List<SuiteDto>>(Messages.SuitesNotFound);

        var suiteDtos = _mapper.Map<List<SuiteDto>>(suites);

        return new SuccessDataResult<List<SuiteDto>>(suiteDtos, Messages.SuitesListed);
    }

    public IDataResult<SuiteDto> GetById(int id)
    {
        Suite suite = _suiteDal.GetById(id);
        if (suite is null)
            return new ErrorDataResult<SuiteDto>(Messages.SuiteNotFound);

        var suiteDto = _mapper.Map<SuiteDto>(suite);

        return new SuccessDataResult<SuiteDto>(suiteDto, Messages.SuiteListedById);
    }

    [ValidationAspect(typeof(SuiteDtoForAddValidator))]
    public IResult Update(SuiteDto suiteDto)
    {
        Suite suite = _suiteDal.GetById(suiteDto.SuiteId);
        if (suite is null)
            return new ErrorResult(Messages.SuiteNotFound);

        suite.Title = suiteDto.Title;
        suite.Bed = suiteDto.Bed;
        suite.M2 = suiteDto.M2;
        suite.Price = suiteDto.Price;
        suite.Vat = suiteDto.Vat;
        suite.TotalVat = CalculateTotalVat(suiteDto.Price, suiteDto.Vat);
        suite.TotalPrice = suiteDto.Price + suite.TotalVat;
        suite.UpdatedAt = DateTimeOffset.Now;
        _suiteDal.Update(suite);

        return new SuccessResult(Messages.SuiteUpdated);
    }

    private decimal CalculateTotalVat(decimal price, decimal vat) 
    {
        return price * vat / 100;
    }
}
