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

public class ContactFormBl : IContactFormBl
{
    private readonly IMapper _mapper;
    private readonly IContactFormDal _contactFormDal;

    public ContactFormBl(
        IMapper mapper,
        IContactFormDal contactFormDal
    )
    {
        _mapper = mapper;
        _contactFormDal = contactFormDal;
    }

    [ValidationAspect(typeof(ContactFormDtoForAddValidator))]
    public IDataResult<ContactFormDto> Add(ContactFormDto contactFormDto)
    {
        ContactForm searchedContactForm = _contactFormDal.GetByMessage(contactFormDto.Message);
        if (searchedContactForm is not null)
            return new ErrorDataResult<ContactFormDto>(Messages.ContactFormAlreadyExists);

        var addedContactForm = _mapper.Map<ContactForm>(contactFormDto);

        addedContactForm.CreatedAt = DateTimeOffset.Now;
        addedContactForm.UpdatedAt = DateTimeOffset.Now;
        long id = _contactFormDal.Add(addedContactForm);
        addedContactForm.ContactFormId = id;

        var addedContactFormDto = _mapper.Map<ContactFormDto>(addedContactForm);

        return new SuccessDataResult<ContactFormDto>(addedContactFormDto, Messages.ContactFormAdded);
    }

    public IResult Delete(long id)
    {
        var getContactFormResult = GetById(id);
        if (getContactFormResult is null)
            return getContactFormResult;

        _contactFormDal.Delete(id);

        return new SuccessResult(Messages.ContactFormDeleted);
    }

    public IDataResult<List<ContactFormDto>> GetAll()
    {
        List<ContactForm> contactForms = _contactFormDal.GetAll();
        if (!contactForms.Any())
            return new ErrorDataResult<List<ContactFormDto>>(Messages.ContactFormsNotFound);

        var contactFormDtos = _mapper.Map<List<ContactFormDto>>(contactForms);

        return new SuccessDataResult<List<ContactFormDto>>(contactFormDtos, Messages.ContactFormsListed);
    }

    public IDataResult<ContactFormDto> GetById(long id)
    {
        ContactForm contactForm = _contactFormDal.GetById(id);
        if (contactForm is null)
            return new ErrorDataResult<ContactFormDto>(Messages.ContactFormNotFound);

        var contactFormDto = _mapper.Map<ContactFormDto>(contactForm);

        return new SuccessDataResult<ContactFormDto>(contactFormDto, Messages.ContactFormListedById);
    }
}
