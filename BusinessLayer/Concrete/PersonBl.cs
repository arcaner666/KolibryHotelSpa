﻿using AutoMapper;
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
        Person searchedPerson = _personDal.GetByEmailOrPhone(personDto.Email, personDto.Phone);
        if (searchedPerson is not null)
            return new ErrorDataResult<PersonDto>(Messages.PersonAlreadyExists);

        HashingHelper.CreatePasswordHash(personDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var addedPerson = _mapper.Map<Person>(personDto);

        addedPerson.PasswordHash = passwordHash;
        addedPerson.PasswordSalt = passwordSalt;
        addedPerson.Blocked = false;
        addedPerson.RefreshToken = "";
        addedPerson.RefreshTokenExpiryTime = DateTimeOffset.Now;
        addedPerson.CreatedAt = DateTimeOffset.Now;
        addedPerson.UpdatedAt = DateTimeOffset.Now;
        long id = _personDal.Add(addedPerson);
        addedPerson.PersonId = id;

        var addedPersonDto = _mapper.Map<PersonDto>(addedPerson);

        return new SuccessDataResult<PersonDto>(addedPersonDto, Messages.PersonAdded);
    }

    public IResult Delete(long id)
    {
        var getPersonResult = GetById(id);
        if (getPersonResult is null)
            return getPersonResult;

        _personDal.Delete(id);

        return new SuccessResult(Messages.PersonDeleted);
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

    public IDataResult<List<PersonExtDto>> GetExts()
    {
        List<PersonExt> personExts = _personDal.GetExts();
        if (!personExts.Any())
            return new ErrorDataResult<List<PersonExtDto>>(Messages.PersonNotFound);

        var personExtDtos = _mapper.Map<List<PersonExtDto>>(personExts);

        return new SuccessDataResult<List<PersonExtDto>>(personExtDtos, Messages.PersonExtsListed);
    }

    public IResult Update(PersonDto personDto)
    {
        Person person = _personDal.GetById(personDto.PersonId);
        if (person is null)
            return new ErrorResult(Messages.PersonNotFound);

        person.Role = personDto.Role;
        person.Blocked = personDto.Blocked;
        person.RefreshToken = personDto.RefreshToken;
        person.RefreshTokenExpiryTime = personDto.RefreshTokenExpiryTime;
        person.UpdatedAt = DateTimeOffset.Now;
        _personDal.Update(person);

        return new SuccessResult(Messages.PersonUpdated);
    }
}
