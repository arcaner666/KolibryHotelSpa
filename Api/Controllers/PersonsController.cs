using BusinessLayer.Abstract;
using BusinessLayer.Extensions;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/persons/")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IPersonAdvBl _personAdvBl;

    public PersonsController(
        IPersonAdvBl personAdvBl
    )
    {
        _personAdvBl = personAdvBl;
    }

    [HttpPost("loginwithemail")]
    public IActionResult LoginWithEmail(PersonExtDto personExtDto)
    {
        var result = _personAdvBl.LoginWithEmail(personExtDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("loginwithphone")]
    public IActionResult LoginWithPhone(PersonExtDto personExtDto)
    {
        var result = _personAdvBl.LoginWithPhone(personExtDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        var result = _personAdvBl.Logout(Convert.ToInt32(HttpContext.User.ClaimPersonId().FirstOrDefault()));
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("refreshaccesstoken")]
    [Authorize]
    public IActionResult RefreshAccessToken(PersonExtDto personExtDto)
    {
        var result = _personAdvBl.RefreshAccessToken(personExtDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("register")]
    public IActionResult Register(PersonExtDto personExtDto)
    {
        var result = _personAdvBl.Register(personExtDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
