using BusinessLayer.Abstract;
using BusinessLayer.Extensions;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/suites/")]
[ApiController]
public class SuitesController : ControllerBase
{
    private readonly ISuiteBl _suiteBl;

    public SuitesController(
        ISuiteBl suiteBl
    )
    {
        _suiteBl = suiteBl;
    }

    [HttpPost("add")]
    public IActionResult Add(SuiteDto suiteDto)
    {
        var result = _suiteBl.Add(suiteDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        var result = _suiteBl.Delete(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _suiteBl.GetAll();
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbyid/{id}")]
    public IActionResult GetById(int id)
    {
        var result = _suiteBl.GetById(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("update")]
    public IActionResult Update(SuiteDto suiteDto)
    {
        var result = _suiteBl.Update(suiteDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
