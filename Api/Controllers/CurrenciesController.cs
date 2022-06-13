﻿using BusinessLayer.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/currencies/")]
[ApiController]
public class CurrenciesController : ControllerBase
{
    private readonly ICurrencyBl _currencyBl;

    public CurrenciesController(
        ICurrencyBl currencyBl
    )
    {
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
}
