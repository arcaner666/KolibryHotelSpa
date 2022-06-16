using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Aspects.Autofac.Transaction;
using BusinessLayer.Aspects.Autofac.Validation;
using BusinessLayer.Constants;
using BusinessLayer.Extensions;
using BusinessLayer.Utilities.Results;
using BusinessLayer.Utilities.Security.Hashing;
using BusinessLayer.Utilities.Security.JWT;
using BusinessLayer.ValidationRules.FluentValidation;
using Entities.DTOs;
using Entities.ExtendedDatabaseModels;

namespace BusinessLayer.Concrete;

public class PersonAdvBl : IPersonAdvBl
{

    private readonly IClaimBl _claimBl;
    private readonly IMapper _mapper;
    private readonly IPersonBl _personBl;
    private readonly IPersonClaimBl _personClaimBl;
    private readonly ITokenService _tokenHelper;

    public PersonAdvBl(
        IClaimBl claimBl,
        IMapper mapper,
        IPersonBl personBl,
        IPersonClaimBl personClaimBl,
        ITokenService tokenHelper
    )
    {
        _claimBl = claimBl;
        _mapper = mapper;
        _personBl = personBl;
        _personClaimBl = personClaimBl;
        _tokenHelper = tokenHelper;
    }

    [ValidationAspect(typeof(PersonExtDtoLoginWithEmailValidator))]
    public IResult LoginWithEmail(PersonExtDto personExtDto)
    {
        var getPersonResult = _personBl.GetByEmail(personExtDto.Email);
        if (!getPersonResult.Success) 
            return getPersonResult;

        var loginResult = Login(personExtDto, getPersonResult.Data);

        return loginResult;
    }

    [ValidationAspect(typeof(PersonExtDtoLoginWithPhoneValidator))]
    public IResult LoginWithPhone(PersonExtDto personExtDto)
    {
        var getPersonResult = _personBl.GetByPhone(personExtDto.Phone);
        if (!getPersonResult.Success) 
            return getPersonResult;

        var loginResult = Login(personExtDto, getPersonResult.Data);

        return loginResult;
    }

    public IResult Logout(long id)
    {
        var getPersonResult = _personBl.GetById(id);
        if (!getPersonResult.Success)
            return getPersonResult;

        getPersonResult.Data.RefreshToken = "";
        getPersonResult.Data.UpdatedAt = DateTimeOffset.Now;
        var updatePersonResult = _personBl.Update(getPersonResult.Data);
        if (!updatePersonResult.Success)
            return updatePersonResult;

        return new SuccessResult(Messages.AuthorizationLoggedOut);
    }

    public IResult RefreshAccessToken(PersonExtDto personExtDto)
    {
        var getClaimsPrincipalResult = _tokenHelper.GetPrincipalFromExpiredToken(personExtDto.AccessToken);
        if (!getClaimsPrincipalResult.Success)
            return getClaimsPrincipalResult;

        List<string> claimRoles = getClaimsPrincipalResult.Data.ClaimRoles();
        List<PersonClaimExtDto> personClaimExtDtos = new();
        foreach (var claimRole in claimRoles)
        {
            personClaimExtDtos.Add(new PersonClaimExtDto
            {
                ClaimTitle = claimRole
            });
        }

        long personId = Convert.ToInt32(getClaimsPrincipalResult.Data.ClaimPersonId().FirstOrDefault());
        var getPersonResult = _personBl.GetById(personId);
        if (!getPersonResult.Success)
            return getPersonResult;
        if (getPersonResult.Data.RefreshToken != personExtDto.RefreshToken)
            return new ErrorResult(Messages.AuthorizationTokenInvalid);
        if (getPersonResult.Data.RefreshTokenExpiryTime <= DateTime.Now)
            return new ErrorResult(Messages.AuthorizationTokenExpired);

        string newAccessToken = _tokenHelper.GenerateAccessToken(getPersonResult.Data.PersonId, personClaimExtDtos);

        personExtDto.AccessToken = newAccessToken;

        return new SuccessDataResult<PersonExtDto>(personExtDto, Messages.AuthorizationTokensRefreshed);
    }

    // Sadece geliştirme aşamasında açık kalmalı çünkü sisteme kullanıcı ekleme işi kontrollü olacak.
    [TransactionScopeAspect]
    [ValidationAspect(typeof(PersonExtDtoRegisterValidator))]
    public IResult Register(PersonExtDto personExtDto)
    {
        PersonDto personDto = new()
        {
            Email = personExtDto.Email,
            Phone = personExtDto.Phone,
            Role = "Admin",
            Password = personExtDto.Password,
        };
        var addPersonResult = _personBl.Add(personDto);
        if (!addPersonResult.Success)
            return addPersonResult;

        var getClaimResult = _claimBl.GetByTitle("Admin");
        if (!getClaimResult.Success)
            return getClaimResult;

        PersonClaimDto personClaimDto = new()
        {
            PersonId = addPersonResult.Data.PersonId,
            ClaimId = getClaimResult.Data.ClaimId,
        };
        var addPersonClaimResult = _personClaimBl.Add(personClaimDto);
        if (!addPersonClaimResult.Success)
            return addPersonClaimResult;

        return new SuccessResult(Messages.AuthorizationRegistered);
    }

    private IResult Login(PersonExtDto personExtDto, PersonDto personDto)
    {
        if (!HashingHelper.VerifyPasswordHash(personExtDto.Password, personDto.PasswordHash, personDto.PasswordSalt))
            return new ErrorDataResult<PersonExtDto>(Messages.AuthorizationWrongPassword);

        var getPersonClaimExtResult = _personClaimBl.GetExtsByPersonId(personDto.PersonId);
        if (!getPersonClaimExtResult.Success)
            return getPersonClaimExtResult;

        string accessToken = _tokenHelper.GenerateAccessToken(personDto.PersonId, getPersonClaimExtResult.Data);
        string refreshToken = _tokenHelper.GenerateRefreshToken();

        personDto.RefreshToken = refreshToken;
        personDto.RefreshTokenExpiryTime = DateTime.Now.AddSeconds(personExtDto.RefreshTokenDuration);
        personDto.UpdatedAt = DateTimeOffset.Now;
        var updatePersonResult = _personBl.Update(personDto);
        if (!updatePersonResult.Success)
            return updatePersonResult;

        var getUpdatedPersonResult = _personBl.GetById(personDto.PersonId);
        if (!getUpdatedPersonResult.Success)
            return getUpdatedPersonResult;

        var personExtDtoResponse = _mapper.Map<PersonExtDto>(getUpdatedPersonResult.Data);
        
        personExtDtoResponse.AccessToken = accessToken;

        return new SuccessDataResult<PersonExtDto>(personExtDtoResponse, Messages.AuthorizationLoggedIn);
    }
}
