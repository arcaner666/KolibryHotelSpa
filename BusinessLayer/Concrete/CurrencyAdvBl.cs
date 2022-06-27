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
using System.Xml;

namespace BusinessLayer.Concrete;

public class CurrencyAdvBl : ICurrencyAdvBl
{


    public CurrencyAdvBl(

    )
    {

    }

    //public IResult UpdateExchangeRates()
    //{
    //    using WebClient client = new();
    //    client.Headers.Add("Content-Type", "application/json");
    //    string result = client.DownloadString("https://evds2.tcmb.gov.tr/service/evds/series=TP.DK.USD.A&startDate=01-10-2017&endDate=01-11-2017&type=xml&key=1zUB6JL6D3");
    //    dynamic json = JValue.Parse(result);

    //    //List<Currency> currencies = _currencyDal.GetAll();
    //    //if (!currencies.Any())
    //    //    return new ErrorDataResult<List<CurrencyDto>>(Messages.CurrenciesNotFound);

    //    //var currencyDtos = _mapper.Map<List<CurrencyDto>>(currencies);

    //    //return new SuccessDataResult<List<CurrencyDto>>(currencyDtos, Messages.CurrenciesListed);
    //    _logger.Error(result);
    //    return new SuccessDataResult<string>(result, Messages.CurrencyExchangeRatesUpdated);
    //}

    public IResult UpdateExchangeRates()
    {
        ExchangeRateRequestDto exchangeRateRequestDto = new();

        string tcmbLink = string.Format("https://www.tcmb.gov.tr/kurlar/today.xml");
        XmlDocument doc = new();
        doc.Load(tcmbLink);
        if(doc.SelectNodes("Tarih_Date").Count > 1)
        {
            return new ErrorDataResult<string>(Messages.CurrencyExchangeRatesCanNotUpdated);
        }
        return new SuccessDataResult<string>(doc.ToString(), Messages.CurrencyExchangeRatesUpdated);
    }
}
