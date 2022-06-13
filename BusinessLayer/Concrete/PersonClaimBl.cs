using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Entities.DTOs;
using Entities.ExtendedDatabaseModels;

namespace BusinessLayer.Concrete;

public class PersonClaimBl : IPersonClaimBl
{
    private readonly IMapper _mapper;
    private readonly IPersonClaimDal _personClaimDal;

    public PersonClaimBl(
        IMapper mapper,
        IPersonClaimDal personClaimDal
    )
    {
        _mapper = mapper;
        _personClaimDal = personClaimDal;
    }

    public IDataResult<PersonClaimDto> Add(PersonClaimDto personClaimDto)
    {
        PersonClaim searchedPersonClaim = _personClaimDal.GetByPersonIdAndClaimId(personClaimDto.PersonId, personClaimDto.ClaimId);
        if (searchedPersonClaim is not null)
            return new ErrorDataResult<PersonClaimDto>(Messages.PersonClaimAlreadyExists);

        var addedPersonClaim = _mapper.Map<PersonClaim>(personClaimDto);

        addedPersonClaim.CreatedAt = DateTimeOffset.Now;
        addedPersonClaim.UpdatedAt = DateTimeOffset.Now;
        long id = _personClaimDal.Add(addedPersonClaim);
        addedPersonClaim.PersonClaimId = id;

        var addedPersonClaimDto = _mapper.Map<PersonClaimDto>(addedPersonClaim);

        return new SuccessDataResult<PersonClaimDto>(addedPersonClaimDto, Messages.PersonClaimAdded);
    }

    public IDataResult<List<PersonClaimExtDto>> GetExtsByPersonId(long personId)
    {
        List<PersonClaimExt> personClaimExts = _personClaimDal.GetExtsByPersonId(personId);
        if (!personClaimExts.Any())
            return new ErrorDataResult<List<PersonClaimExtDto>>(Messages.PersonClaimsNotFound);

        var personClaimExtDtos = _mapper.Map<List<PersonClaimExtDto>>(personClaimExts);

        return new SuccessDataResult<List<PersonClaimExtDto>>(personClaimExtDtos, Messages.PersonClaimExtsListedByPersonId);
    }
}
