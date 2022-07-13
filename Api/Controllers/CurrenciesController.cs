using BusinessLayer.Abstract;
using BusinessLayer.Aspects.Autofac.Logging;
using BusinessLayer.CrossCuttingConcerns.Logging.NLog;
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
}
