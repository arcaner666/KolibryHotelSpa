using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using BusinessLayer.Utilities.Security.Hashing;
using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Entities.DTOs;

namespace BusinessLayer.Concrete;

public class PersonBl : IPersonBl
{
    private readonly IMapper _mapper;
    private readonly IPersonDal _personDal;

    public PersonBl(
        IMapper mapper,
        IPersonDal personDal
    )
    {
        _mapper = mapper;
        _personDal = personDal;
    }

    public IDataResult<PersonDto> Add(PersonDto personDto)
    {
        Person searchedPerson = _personDal.GetByEmail(personDto.Email);
        if (searchedPerson is not null)
            return new ErrorDataResult<PersonDto>(Messages.PersonAlreadyExists);

        HashingHelper.CreatePasswordHash(personDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var addedPerson = _mapper.Map<Person>(personDto);

        addedPerson.UserName = "";
        addedPerson.Phone = "";
        addedPerson.PasswordHash = passwordHash;
        addedPerson.PasswordSalt = passwordSalt;
        addedPerson.Blocked = false;
        addedPerson.RefreshToken = "";
        addedPerson.RefreshTokenExpiryTime = DateTime.Now;
        addedPerson.CreatedAt = DateTimeOffset.Now;
        addedPerson.UpdatedAt = DateTimeOffset.Now;
        long id = _personDal.Add(addedPerson);
        addedPerson.PersonId = id;

        var addedPersonDto = _mapper.Map<PersonDto>(addedPerson);

        return new SuccessDataResult<PersonDto>(addedPersonDto, Messages.PersonAdded);
    }

    public IDataResult<PersonDto> GetByEmail(string email)
    {
        Person person = _personDal.GetByEmail(email);
        if (person is null)
            return new ErrorDataResult<PersonDto>(Messages.PersonNotFound);

        var personDto = _mapper.Map<PersonDto>(person);

        return new SuccessDataResult<PersonDto>(personDto, Messages.PersonListedByEmail);
    }

    public IDataResult<PersonDto> GetById(long id)
    {
        Person person = _personDal.GetById(id);
        if (person is null)
            return new ErrorDataResult<PersonDto>(Messages.PersonNotFound);

        var personDto = _mapper.Map<PersonDto>(person);

        return new SuccessDataResult<PersonDto>(personDto, Messages.PersonListedById);
    }

    public IDataResult<PersonDto> GetByPhone(string phone)
    {
        Person person = _personDal.GetByPhone(phone);
        if (person is null)
            return new ErrorDataResult<PersonDto>(Messages.PersonNotFound);

        var personDto = _mapper.Map<PersonDto>(person);

        return new SuccessDataResult<PersonDto>(personDto, Messages.PersonListedByPhone);
    }

    public IResult Update(PersonDto personDto)
    {
        Person person = _personDal.GetById(personDto.PersonId);
        if (person is null)
            return new ErrorDataResult<PersonDto>(Messages.PersonNotFound);

        person.UserName = personDto.UserName;
        person.Phone = personDto.Phone;
        person.Role = personDto.Role;
        person.Blocked = personDto.Blocked;
        person.RefreshToken = personDto.RefreshToken;
        person.RefreshTokenExpiryTime = personDto.RefreshTokenExpiryTime;
        person.UpdatedAt = DateTimeOffset.Now;
        _personDal.Update(person);

        return new SuccessResult(Messages.PersonUpdated);
    }
}
