using BusinessLayer.Abstract;
using BusinessLayer.Extensions;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/invoices/")]
[ApiController]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceAdvBl _invoiceAdvBl;
    private readonly IInvoiceBl _invoiceBl;

    public InvoicesController(
        IInvoiceAdvBl invoiceAdvBl,
        IInvoiceBl invoiceBl
    )
    {
        _invoiceAdvBl = invoiceAdvBl;
        _invoiceBl = invoiceBl;
    }

    [HttpPost("add")]
    public IActionResult Add(InvoiceExtDto invoiceExtDto)
    {
        var result = _invoiceAdvBl.Add(invoiceExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        var result = _invoiceAdvBl.Delete(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbyid/{id}")]
    public IActionResult GetById(int id)
    {
        var result = _invoiceBl.GetById(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getexts")]
    public IActionResult GetExts()
    {
        var result = _invoiceBl.GetExts();
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("update")]
    public IActionResult Update(InvoiceExtDto invoiceExtDto)
    {
        var result = _invoiceAdvBl.Update(invoiceExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
