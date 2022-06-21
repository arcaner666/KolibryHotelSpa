using BusinessLayer.Abstract;
using BusinessLayer.Extensions;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
}
