using BusinessLayer.Abstract;
using BusinessLayer.Extensions;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace Api.Controllers;

[Route("api/paytr/")]
[ApiController]
public class PayTrController : ControllerBase
{
    private readonly IPayTrBl _payTrBl;

    public PayTrController(
        IPayTrBl payTrBl
    )
    {
        _payTrBl = payTrBl;
    }

    [HttpPost("getiframetoken")]
    public IActionResult GetIframeToken(PayTrIframeDto payTrIframeDto)
    {
        var result = _payTrBl.GetIframeToken(payTrIframeDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("setpaymentresult")]
    public string SetPaymentResult()
    {
        var requestForm = Request.Form;
        var result = _payTrBl.SetPaymentResult(requestForm);
        return result;
    }
}
