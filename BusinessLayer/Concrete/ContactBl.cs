using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Aspects.Autofac.Validation;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using BusinessLayer.Utilities.Security.Hashing;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.Abstract;
using Entities.DatabaseModels;
using Entities.DTOs;

namespace BusinessLayer.Concrete;

public class ContactBl : IContactBl
{
    private readonly IMapper _mapper;
    private readonly IContactDal _contactDal;

    public ContactBl(
        IMapper mapper,
        IContactDal contactDal
    )
    {
        _mapper = mapper;
        _contactDal = contactDal;
    }

    [ValidationAspect(typeof(ContactDtoForAddValidator))]
    public IDataResult<ContactDto> Add(ContactDto contactDto)
    {
        Contact searchedContact = _contactDal.GetByMessage(contactDto.Message);
        if (searchedContact is not null)
            return new ErrorDataResult<ContactDto>(Messages.ContactAlreadyExists);

        var addedContact = _mapper.Map<Contact>(contactDto);

        addedContact.CreatedAt = DateTimeOffset.Now;
        addedContact.UpdatedAt = DateTimeOffset.Now;
        long id = _contactDal.Add(addedContact);
        addedContact.ContactId = id;

        var addedContactDto = _mapper.Map<ContactDto>(addedContact);

        return new SuccessDataResult<ContactDto>(addedContactDto, Messages.ContactAdded);
    }

    public IResult Delete(long id)
    {
        var getContactResult = GetById(id);
        if (getContactResult is null)
            return getContactResult;

        _contactDal.Delete(id);

        return new SuccessResult(Messages.ContactDeleted);
    }

    public IDataResult<List<ContactDto>> GetAll()
    {
        List<Contact> contacts = _contactDal.GetAll();
        if (!contacts.Any())
            return new ErrorDataResult<List<ContactDto>>(Messages.ContactsNotFound);

        var contactDtos = _mapper.Map<List<ContactDto>>(contacts);

        return new SuccessDataResult<List<ContactDto>>(contactDtos, Messages.ContactsListed);
    }

    public IDataResult<ContactDto> GetById(long id)
    {
        Contact contact = _contactDal.GetById(id);
        if (contact is null)
            return new ErrorDataResult<ContactDto>(Messages.ContactNotFound);

        var contactDto = _mapper.Map<ContactDto>(contact);

        return new SuccessDataResult<ContactDto>(contactDto, Messages.ContactListedById);
    }
}
