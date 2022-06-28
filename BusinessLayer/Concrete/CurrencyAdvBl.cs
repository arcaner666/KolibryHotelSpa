using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.CrossCuttingConcerns.Logging;
using BusinessLayer.Utilities.Results;
using Entities.DTOs;
using System.Xml;

namespace BusinessLayer.Concrete;

public class CurrencyAdvBl : ICurrencyAdvBl
{
    private readonly ICurrencyBl _currencyBl;
    private readonly ILoggerManager _loggerManager;

    public CurrencyAdvBl(
        ICurrencyBl currencyBl,
        ILoggerManager loggerManager
    )
    {
        _currencyBl = currencyBl;
        _loggerManager = loggerManager;
    }

    public IDataResult<List<CurrencyDto>> GetAll()
    {
        var updateCurrenciesResult = UpdateExchangeRatesFromTcmb();
        if (!updateCurrenciesResult.Success)
            return new ErrorDataResult<List<CurrencyDto>>(updateCurrenciesResult.Message);

        var getCurrenciesResult = _currencyBl.GetAll();
        if (!getCurrenciesResult.Success)
            return getCurrenciesResult;

        return new SuccessDataResult<List<CurrencyDto>>(getCurrenciesResult.Data, Messages.CurrenciesListed);
    }

    // https://openexchangerates.org/ Ücretsiz planda aylık 1000 request limiti var ve referans döviz USD.
    private IResult UpdateExchangeRatesFromOpenExchangeRates()
    {
        string sourceLink = "https://openexchangerates.org/api/latest.json?";

        // Veri çekebilmek için zorunlu. Sitedeki dashboard üzerinden edinilebilir.
        string appId = "299780a7bcac4bf690461e6ff2b1e101";

        using HttpClient client = new();
        var request = new HttpRequestMessage(HttpMethod.Get, sourceLink);
        request.Headers.Add("Authorization", $"Token {appId}");
        var response = client.Send(request);
        return new ErrorResult(Messages.CurrencyExchangeRatesCanNotRetrieveFromSource);
        if (!response.IsSuccessStatusCode)
            return new ErrorResult(Messages.CurrencyExchangeRatesCanNotRetrieveFromSource);
        // DÖVİZLER KAYNAKTAN GETİRİLİYOR FAKAT VERİ TABANINDAKİ KURLARI GÜNCELLEME İŞLEMİ YAPILMADI SONRA YAPILACAK!!!
    }

    // https://tcmb.gov.tr/kurlar/today.xml Ücretsiz ve limitsiz fakat bir gün öncenin verilerini veriyor, günlük güncelleniyor.
    private IResult UpdateExchangeRatesFromTcmb()
    {
        var getCurrenciesResult = _currencyBl.GetAll();
        if (!getCurrenciesResult.Success)
            return new ErrorResult(Messages.CurrenciesNotFound);

        // Zamansal hataları yakalamak için test satırı
        //return new ErrorResult($"DateTimeOffset.Now: {DateTimeOffset.Now.ToUnixTimeSeconds()} | UpdatedAt: {getCurrenciesResult.Data[0].UpdatedAt.ToUnixTimeSeconds()} | Difference: {DateTimeOffset.Now.ToUnixTimeSeconds() - getCurrenciesResult.Data[0].UpdatedAt.ToUnixTimeSeconds()}");
        if (DateTimeOffset.Now.ToUnixTimeSeconds() - getCurrenciesResult.Data.Find(c => c.AlphabeticCode == "USD").UpdatedAt.ToUnixTimeSeconds() < 43200) // 12 saatte bir
            return new SuccessResult(Messages.CurrencyExchangeRatesAreUpToDate);

        // Gelen verinin formatı aşağıdaki gibidir.
        //<? xml version = "1.0" encoding = "UTF-8" ?>
        //<? xml - stylesheet type = "text/xsl" href = "isokur.xsl" ?>
        //< Tarih_Date Tarih = "27.06.2022" Date = "06/27/2022" Bulten_No = "2022/122" >

        //    < Currency CrossOrder = "0" Kod = "USD" CurrencyCode = "USD" >
        //        < Unit > 1 </ Unit >
        //        < Isim > ABD DOLARI </ Isim >
        //        < CurrencyName > US DOLLAR </ CurrencyName >
        //        < ForexBuying > 16.6460 </ ForexBuying >
        //        < ForexSelling > 16.6760 </ ForexSelling >
        //        < BanknoteBuying > 16.6343 </ BanknoteBuying >
        //        < BanknoteSelling > 16.7010 </ BanknoteSelling >
        //        < CrossRateUSD />
        //        < CrossRateOther />
        //    </ Currency >

        //    < Currency CrossOrder = "9" Kod = "EUR" CurrencyCode = "EUR" >
        //        < Unit > 1 </ Unit >
        //        < Isim > EURO </ Isim >
        //        < CurrencyName > EURO </ CurrencyName >
        //        < ForexBuying > 17.6057 </ ForexBuying >
        //        < ForexSelling > 17.6375 </ ForexSelling >
        //        < BanknoteBuying > 17.5934 </ BanknoteBuying >
        //        < BanknoteSelling > 17.6639 </ BanknoteSelling >
        //        < CrossRateUSD />
        //        < CrossRateOther > 1.0577 </ CrossRateOther >
        //    </ Currency >

        //    < Currency CrossOrder = "10" Kod = "GBP" CurrencyCode = "GBP" >
        //        < Unit > 1 </ Unit >
        //        < Isim > İNGİLİZ STERLİNİ </ Isim >
        //        < CurrencyName > POUND STERLING </ CurrencyName >
        //        < ForexBuying > 20.4246 </ ForexBuying >
        //        < ForexSelling > 20.5311 </ ForexSelling >
        //        < BanknoteBuying > 20.4103 </ BanknoteBuying >
        //        < BanknoteSelling > 20.5619 </ BanknoteSelling >
        //        < CrossRateUSD />
        //        < CrossRateOther > 1.2291 </ CrossRateOther >
        //    </ Currency >
        //</ Tarih_Date >

        // Örneğin 25/04/2022 tarihinin kurlarını getirmek için;
        //string tcmbLink = https://tcmb.gov.tr/kurlar/202204/25042022.xml;

        // Bu günün kurlarını getirmek için;
        string sourceLink = "https://tcmb.gov.tr/kurlar/today.xml";

        using HttpClient client = new();
        var task = Task.Run(() => client.GetAsync(sourceLink));
        task.Wait();
        var response = task.Result;
        if (!response.IsSuccessStatusCode)
            return new ErrorResult(Messages.CurrencyExchangeRatesCanNotRetrieveFromSource);

        var xmlStream = response.Content.ReadAsStream();
        XmlDocument xmlDocument = new();
        xmlDocument.Load(xmlStream);

        _loggerManager.LogInfo(xmlDocument.OuterXml);

        foreach (XmlNode node in xmlDocument.SelectNodes("Tarih_Date")[0].ChildNodes)
        {
            if (node.Attributes["Kod"].Value == "USD")
            {
                var filteredCurrency = getCurrenciesResult.Data.Find(c => c.AlphabeticCode == "USD");
                filteredCurrency.ExchangeRate = Convert.ToDecimal(node["ForexSelling"].InnerText);
                var updateCurrencyResult = _currencyBl.Update(filteredCurrency);
                if (!updateCurrencyResult.Success)
                    return new ErrorResult(Messages.CurrencyExchangeRatesCanNotUpdated);
            };
            
            if (node.Attributes["Kod"].Value == "EUR")
            {
                var filteredCurrency = getCurrenciesResult.Data.Find(c => c.AlphabeticCode == "EUR");
                filteredCurrency.ExchangeRate = Convert.ToDecimal(node["ForexSelling"].InnerText);
                var updateCurrencyResult = _currencyBl.Update(filteredCurrency);
                if (!updateCurrencyResult.Success)
                    return new ErrorResult(Messages.CurrencyExchangeRatesCanNotUpdated);
            };
        }

        return new SuccessResult(Messages.CurrencyExchangeRatesUpdated);
    }
}
