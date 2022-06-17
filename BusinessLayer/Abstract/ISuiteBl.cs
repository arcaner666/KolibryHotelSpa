using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface ISuiteBl
{
    IDataResult<SuiteDto> Add(SuiteDto suiteDto);
    IResult Delete(int id);
    IDataResult<List<SuiteDto>> GetAll();
    IDataResult<SuiteDto> GetById(int id);
    IResult Update(SuiteDto suiteDto);
}
