using BusinessLayer.Abstract;
using BusinessLayer.Extensions;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/contactforms/")]
[ApiController]
public class ContactFormsController : ControllerBase
{
    private readonly IContactFormBl _contactFormBl;

    public ContactFormsController(
        IContactFormBl contactFormBl
    )
    {
        _contactFormBl = contactFormBl;
    }

    [HttpPost("add")]
    public IActionResult Add(ContactFormDto contactFormDto)
    {
        var result = _contactFormBl.Add(contactFormDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(long id)
    {
        var result = _contactFormBl.Delete(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _contactFormBl.GetAll();
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbyid/{id}")]
    public IActionResult GetById(long id)
    {
        var result = _contactFormBl.GetById(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
