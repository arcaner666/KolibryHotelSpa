using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface IPersonAdvBl
{
    IResult Add(PersonExtDto personExtDto);
    IResult Delete(long id);
    IResult LoginWithEmail(PersonExtDto personExtDto);
    IResult LoginWithPhone(PersonExtDto personExtDto);
    IResult Logout(long id);
    IResult RefreshAccessToken(PersonExtDto personExtDto);
    IResult Update(PersonExtDto personExtDto);
}
