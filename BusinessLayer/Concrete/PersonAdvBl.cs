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
        getPersonResult.Data.UpdatedAt = DateTime.Now;
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

    [TransactionScopeAspect]
    public IResult Register(PersonExtDto personExtDto)
    {
        //// Yeni bir kişi eklenir.
        //PersonDto personDto = new()
        //{
        //    Email = personExtDto.Email,
        //    Role = "Employee",
        //};
        //var addPersonResult = _personBl.Add(personDto);
        //if (!addPersonResult.Success) 
        //    return addPersonResult;

        //// Yetki adından yetkinin id'si bulunur.
        //var getClaimResult = _claimBl.GetByTitle("Employee");
        //if (!getClaimResult.Success) 
        //    return getClaimResult;

        //// Personel yetkileri verilir.
        //SystemUserClaimDto systemUserClaimDto = new()
        //{
        //    SystemUserId = addSystemUserResult.Data.SystemUserId,
        //    OperationClaimId = getOperationClaimResult.Data.OperationClaimId,
        //};
        //var addSystemUserClaimResult = _systemUserClaimBl.Add(systemUserClaimDto);
        //if (!addSystemUserClaimResult.Success) 
        //    return addSystemUserClaimResult;

        //// Yeni bir işletme eklenir.
        //BusinessDto businessDto = new()
        //{
        //    OwnerSystemUserId = addSystemUserResult.Data.SystemUserId,
        //    BusinessName = registerSectionManagerDto.BusinessName,
        //};
        //var addBusinessResult = _businessBl.Add(businessDto);
        //if (!addBusinessResult.Success) 
        //    return addBusinessResult;

        //// İşletmenin merkez şubesinin adresi eklenir.
        //FullAddressDto fullAddressDto = new()
        //{
        //    CityId = registerSectionManagerDto.CityId,
        //    DistrictId = registerSectionManagerDto.DistrictId,
        //    AddressTitle = "Merkez",
        //    PostalCode = 0,
        //    AddressText = registerSectionManagerDto.AddressText,
        //};
        //var addFullAddressResult = _fullAddressBl.Add(fullAddressDto);
        //if (!addFullAddressResult.Success) 
        //    return addFullAddressResult;

        //// İşletmenin merkez şubesi eklenir.
        //BranchDto branchDto = new()
        //{
        //    BusinessId = addBusinessResult.Data.BusinessId,
        //    FullAddressId = addFullAddressResult.Data.FullAddressId,
        //    BranchOrder = 1,
        //    BranchName = "Merkez",
        //    BranchCode = "000001",
        //};
        //var addBranchResult = _branchBl.Add(branchDto);
        //if (!addBranchResult.Success) 
        //    return addBranchResult;

        //// Kullanıcı kaydındaki işletme ve şube id'leri güncellenir.
        //addSystemUserResult.Data.BusinessId = addBusinessResult.Data.BusinessId;
        //addSystemUserResult.Data.BranchId = addBranchResult.Data.BranchId;
        //var updateSystemUserResult = _personBl.Update(addSystemUserResult.Data);
        //if (!updateSystemUserResult.Success) 
        //    return updateSystemUserResult;

        //// Kasanın doviz cinsi getirilir.
        //var getCurrencyResult = _currencyBl.GetByCurrencyName("TL");
        //if (!getCurrencyResult.Success)
        //    return getCurrencyResult;

        //// İşletmenin kasası oluşturulur.
        //CashExtDto cashExtDto = new()
        //{
        //    BusinessId = addBusinessResult.Data.BusinessId,
        //    BranchId = addBranchResult.Data.BranchId,
        //    CurrencyId = getCurrencyResult.Data.CurrencyId,
        //    AccountOrder = 1,
        //    AccountName = "TL Kasası",
        //    AccountCode = "10000000100000001",
        //    Limit = 0,
        //};
        //var addCashExtResult = _cashAdvBl.Add(cashExtDto);
        //if (!addCashExtResult.Success)
        //    return addCashExtResult;

        //// Yeni bir yönetici eklenir.
        //ManagerDto managerDto = new()
        //{
        //    BusinessId = addBusinessResult.Data.BusinessId,
        //    BranchId = addBranchResult.Data.BranchId,
        //    NameSurname = registerSectionManagerDto.NameSurname,
        //    Phone = registerSectionManagerDto.Phone,
        //    Email = "",
        //    Gender = "",
        //    Notes = "",
        //    AvatarUrl = "",
        //    TaxOffice = registerSectionManagerDto.TaxOffice,
        //    TaxNumber = registerSectionManagerDto.TaxNumber,
        //    IdentityNumber = registerSectionManagerDto.IdentityNumber,
        //};
        //var addManagerResult = _managerBl.Add(managerDto);
        //if (!addManagerResult.Success) 
        //    return addManagerResult;

        //// Yeni site grubu eklenir.
        //SectionGroupDto sectionGroupDto = new()
        //{
        //    BusinessId = addBusinessResult.Data.BusinessId,
        //    BranchId = addBranchResult.Data.BranchId,
        //    SectionGroupName = "Genel",
        //};
        //var addSectionGroupResult = _sectionGroupBl.Add(sectionGroupDto);
        //if (!addSectionGroupResult.Success) 
        //    return addSectionGroupResult;

        //return new SuccessResult(Messages.AuthorizationSectionManagerRegistered);
        return new ErrorResult("Not Implemented!");
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

        personExtDto.RefreshToken = refreshToken;
        personExtDto.RefreshTokenExpiryTime = DateTime.Now.AddSeconds(personExtDto.RefreshTokenDuration);
        personExtDto.UpdatedAt = DateTimeOffset.Now;
        personExtDto.AccessToken = accessToken;

        return new SuccessDataResult<PersonExtDto>(personExtDto, Messages.AuthorizationLoggedIn);
    }
}
