using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Entities.DTOs;
using Newtonsoft.Json.Linq;
using NLog;
using System.Net;
using System.Text;

namespace BusinessLayer.Concrete;

public class CurrencyAdvBl : ICurrencyAdvBl
{
    private readonly ICurrencyDal _currencyDal;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public CurrencyAdvBl(
        ICurrencyDal currencyDal,
        ILogger logger,
        IMapper mapper
    )
    {
        _currencyDal = currencyDal;
        _logger = logger;
        _mapper = mapper;
    }

    public IResult UpdateExchangeRates()
    {
        using WebClient client = new();
        client.Headers.Add("Content-Type", "application/json");
        string result = client.DownloadString("https://evds2.tcmb.gov.tr/service/evds/series=TP.DK.USD.A&startDate=01-10-2017&endDate=01-11-2017&type=xml&key=1zUB6JL6D3");
        dynamic json = JValue.Parse(result);

        //List<Currency> currencies = _currencyDal.GetAll();
        //if (!currencies.Any())
        //    return new ErrorDataResult<List<CurrencyDto>>(Messages.CurrenciesNotFound);

        //var currencyDtos = _mapper.Map<List<CurrencyDto>>(currencies);

        //return new SuccessDataResult<List<CurrencyDto>>(currencyDtos, Messages.CurrenciesListed);
        return new SuccessDataResult<string>(result, Messages.CurrencyExchangeRatesUpdated);
    }
}
