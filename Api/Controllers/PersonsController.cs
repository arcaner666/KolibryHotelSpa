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
    private readonly IPersonBl _personBl;

    public PersonsController(
        IPersonAdvBl personAdvBl,
        IPersonBl personBl
    )
    {
        _personAdvBl = personAdvBl;
        _personBl = personBl;
    }

    // Sadece geliştirme aşamasında açık kalmalı çünkü sisteme kullanıcı ekleme işi kontrollü olacak.
    [HttpPost("add")]
    public IActionResult Add(PersonExtDto personExtDto)
    {
        var result = _personAdvBl.Add(personExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(long id)
    {
        var result = _personAdvBl.Delete(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getexts")]
    public IActionResult GetExts()
    {
        var result = _personBl.GetExts();
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
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

    //[HttpPost("update")]
    //[Authorize]
    //public IActionResult Update(PersonExtDto personExtDto)
    //{
    //    var result = _personAdvBl.Update(personExtDto);
    //    if (result.Success)
    //        return Ok(result);
    //    return StatusCode(StatusCodes.Status500InternalServerError, result);
    //}
}
