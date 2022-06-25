using BusinessLayer.Abstract;
using Entities.DTOs;
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
        var result = _currencyBl.GetAll();
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("updateexchangerates")]
    public IActionResult UpdateExchangeRates()
    {
        var result = _currencyAdvBl.UpdateExchangeRates();
        if (result.Success)
            return Ok(result);
        //return StatusCode(StatusCodes.Status500InternalServerError, result);
        return BadRequest(result);
    }
}
