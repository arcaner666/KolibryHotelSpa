using BusinessLayer.Utilities.Results;
using Entities.DTOs;

namespace BusinessLayer.Abstract;

public interface IPersonAdvBl
{
    IResult LoginWithEmail(PersonExtDto personExtDto);
    IResult LoginWithPhone(PersonExtDto personExtDto);
    IResult Logout(long id);
    IResult RefreshAccessToken(PersonExtDto personExtDto);
    IResult Register(PersonExtDto personExtDto);
}
