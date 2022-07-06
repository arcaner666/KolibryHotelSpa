using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/currencies/")]
[ApiController]
public class CurrenciesController : ControllerBase
{
    private readonly ICurrencyAdvBl _currencyAdvBl;
    private readonly ICurrencyBl _currencyBl;

    public CurrenciesController(
        ICurrencyAdvBl currencyAdvBl,
        ICurrencyBl currencyBl
    )
    {
        _currencyAdvBl = currencyAdvBl;
        _currencyBl = currencyBl;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _currencyAdvBl.GetAll();
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    //[HttpPost("logerrormethod")]
    //public IActionResult LogErrorMethod(int number)
    //{
    //    LogMethodParameter logMethodParameter = new()
    //    {
    //        Name = "number",
    //        Type = typeof(int).Name,
    //        Value = number.ToString(),
    //    };
    //    LogDetail logDetail = new()
    //    {
    //        FullName = "Api.Controllers.CurrenciesController",
    //        MethodName = "LogErrorMethod",
    //        MethodParameters = new List<LogMethodParameter>() 
    //        { 
    //            logMethodParameter 
    //        },
    //    };

    //    _loggerManager.LogError(logDetail);
    //    var result = "OK";
    //    return Ok(result);
    //}

    [HttpPost("testlogaspect")]
    public IActionResult TestLogAspect(int number, string str)
    {
        var result = _currencyAdvBl.TestLogAspect(number, str);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
