﻿using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Entities.DTOs;

namespace BusinessLayer.Concrete;

public class CurrencyBl : ICurrencyBl
{
    private readonly ICurrencyDal _currencyDal;
    private readonly IMapper _mapper;

    public CurrencyBl(
        ICurrencyDal currencyDal,
        IMapper mapper
    )
    {
        _currencyDal = currencyDal;
        _mapper = mapper;
    }

    public IDataResult<List<CurrencyDto>> GetAll()
    {
        List<Currency> currencies = _currencyDal.GetAll();
        if (!currencies.Any())
            return new ErrorDataResult<List<CurrencyDto>>(Messages.CurrenciesNotFound);

        var currencyDtos = _mapper.Map<List<CurrencyDto>>(currencies);

        return new SuccessDataResult<List<CurrencyDto>>(currencyDtos, Messages.CurrenciesListed);
    }

    public IDataResult<CurrencyDto> GetByCurrencyName(string currencyName)
    {
        Currency currency = _currencyDal.GetByCurrencyName(currencyName);
        if (currency is null)
            return new ErrorDataResult<CurrencyDto>(Messages.CurrencyNotFound);

        var currencyDto = _mapper.Map<CurrencyDto>(currency);

        return new SuccessDataResult<CurrencyDto>(currencyDto, Messages.CurrencyListedByTitle);
    }
}
