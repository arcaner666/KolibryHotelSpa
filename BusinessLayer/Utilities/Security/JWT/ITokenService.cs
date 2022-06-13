using BusinessLayer.Utilities.Results;
using Entities.ExtendedDatabaseModels;
using System.Security.Claims;

namespace BusinessLayer.Utilities.Security.JWT;

public interface ITokenService
{
    string GenerateAccessToken(long systemUserId, List<PersonClaimExtDto> personClaimExtDtos);
    string GenerateRefreshToken();
    IDataResult<ClaimsPrincipal> GetPrincipalFromExpiredToken(string accessToken);
}
