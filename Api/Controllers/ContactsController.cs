using BusinessLayer.Abstract;
using BusinessLayer.Extensions;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/contacts/")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactBl _contactBl;

    public ContactsController(
        IContactBl contactBl
    )
    {
        _contactBl = contactBl;
    }

    [HttpPost("add")]
    public IActionResult Add(ContactDto contactDto)
    {
        var result = _contactBl.Add(contactDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(long id)
    {
        var result = _contactBl.Delete(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _contactBl.GetAll();
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbyid/{id}")]
    public IActionResult GetById(long id)
    {
        var result = _contactBl.GetById(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
